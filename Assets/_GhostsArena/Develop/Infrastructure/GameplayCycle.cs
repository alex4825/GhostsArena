using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayCycle : IDisposable
{
    private MainHeroSpawner _mainHeroSpawner;
    private LevelConfig _levelConfig;
    private ConfirmPopup _confirmPopup;
    private MonoBehaviour _context;
    private List<EnemiesSpawner> _enemiesSpawners;
    private Transform _mainHeroSpawnPoint;

    private GameMode _gameMode;
    private CharacterControllerCharacter _mainHero;

    private int _killedEnemies;
    private int _spawnedEnemies;
    private int _aliveEnemies;
    private float _playTimer;

    public GameplayCycle(
        MainHeroSpawner mainHeroSpawner,
        LevelConfig levelConfig,
        ConfirmPopup confirmPopup,
        MonoBehaviour context,
        List<EnemiesSpawner> enemiesSpawners,
        Transform mainHeroSpawnPoint)
    {
        _mainHeroSpawner = mainHeroSpawner;
        _levelConfig = levelConfig;
        _confirmPopup = confirmPopup;
        _context = context;
        _enemiesSpawners = enemiesSpawners;
        _mainHeroSpawnPoint = mainHeroSpawnPoint;

        foreach (var spawner in _enemiesSpawners)
            spawner.Spawned += OnEnemySpawned;
    }

    public IEnumerator Launch()
    {
        yield return _context.StartCoroutine(_mainHeroSpawner.Spawn(hero => _mainHero = hero, _mainHeroSpawnPoint.position));

        _confirmPopup.Show();
        _confirmPopup.ShowMessage($"Press {KeyCode.F.ToString()} for begin");

        yield return _confirmPopup.WaitConfirm(KeyCode.F);

        _confirmPopup.Hide();

        _gameMode = new(_mainHero, _enemiesSpawners);

        _gameMode.Win += OnGameModeWin;
        _gameMode.Defeat += OnGameModeDefeat;

        _gameMode.Start(GetWinCondition(), GetDefeatCondition());
    }

    public void Update()
    {
        _playTimer += Time.deltaTime;

        _gameMode?.Update();
    }

    public void Dispose()
    {
        OnGameModeEnded();

        foreach (var spawner in _enemiesSpawners)
            spawner.Spawned -= OnEnemySpawned;
    }

    private void OnGameModeEnded()
    {
        if (_gameMode != null)
        {
            Debug.Log($"Time passed: {_playTimer:F2}. Enemy killed: {_killedEnemies}. Enemy spawned: {_spawnedEnemies}.");

            _killedEnemies = 0;
            _spawnedEnemies = 0;
            _playTimer = 0;

            _gameMode.Win -= OnGameModeWin;
            _gameMode.Defeat -= OnGameModeDefeat;
        }
    }

    private void OnGameModeWin()
    {
        OnGameModeEnded();
        Debug.Log($"Win.");
        _context.StartCoroutine(Launch());
    }

    private void OnGameModeDefeat()
    {
        OnGameModeEnded();
        Debug.Log($"Defeat.");
        _context.StartCoroutine(Launch());
    }

    private Func<bool> GetWinCondition()
    {
        switch (_levelConfig.WinCondition)
        {
            case WinConditions.SurviveThroughTime:
                return () => _playTimer >= _levelConfig.TimeToWin;

            case WinConditions.KillEnemiesCount:
                return () => _killedEnemies >= _levelConfig.KillEnemiesToWin;

            default:
                throw new InvalidOperationException($"There is no such condition of win like {_levelConfig.WinCondition}");
        }
    }

    private Func<bool> GetDefeatCondition()
    {
        switch (_levelConfig.DefeatCondition)
        {
            case DefeatConditions.MainHeroDied:
                return () => _mainHero.IsDead;

            case DefeatConditions.TooMuchEnemies:
                return () => _aliveEnemies >= _levelConfig.MaxSpawnedEnemies;

            default:
                throw new InvalidOperationException($"There is no such condition of defeat like {_levelConfig.DefeatCondition}");
        }
    }

    private void OnEnemySpawned(AgentEnemyCharacter enemy)
    {
        _aliveEnemies++;
        _spawnedEnemies++;
        enemy.Killed += OnEnemyKilled;
        enemy.Dead += OnEnemyDead;
    }

    private void OnEnemyDead(IKillable enemy)
    {
        _aliveEnemies--;
        enemy.Dead -= OnEnemyDead;
    }

    private void OnEnemyKilled(IKillable enemy)
    {
        _killedEnemies++;
        enemy.Killed -= OnEnemyKilled;
    }
}
