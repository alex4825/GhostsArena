using System;
using System.Collections.Generic;
using UnityEngine;

public class GameMode
{
    private LevelConfig _levelConfig;
    private CharacterControllerCharacter _mainHero;
    private List<EnemiesSpawner> _enemySpawners;

    private List<AgentEnemyCharacter> _enemies = new();

    private int _killedEnemies;
    private int _spawnedEnemies;
    private float _playTimer;

    private bool _isRunning;

    public event Action Win;
    public event Action Defeat;

    private Func<bool> WinCondition;
    private Func<bool> DefeatCondition;

    public GameMode(LevelConfig levelConfig, CharacterControllerCharacter mainHero, List<EnemiesSpawner> enemySpawners)
    {
        _levelConfig = levelConfig;
        _mainHero = mainHero;
        _enemySpawners = enemySpawners;
    }

    public void Start()
    {
        SetWinCondition();

        SetDefeatCondition();

        foreach (var spawner in _enemySpawners)
        {
            spawner.ActivateSpawnProcess(_mainHero);
            spawner.Spawned += OnEnemySpawned;
        }

        _isRunning = true;
    }

    public void Update()
    {
        if (_isRunning == false)
            return;

        _playTimer += Time.deltaTime;

        if (WinCondition?.Invoke() == true)
        {
            ProcessWin();
            Win?.Invoke();
        }

        if (DefeatCondition?.Invoke() == true)
        {
            ProcessDefeat();
            Defeat?.Invoke();
        }
    }

    private void ProcessWin()
    {
        Debug.Log($"You win!");

        for (int i = 0; i < _enemies.Count; i++)
            _enemies[i].Kill();

        _enemies.Clear();

        ProcessEndGame();
    }

    private void ProcessDefeat()
    {
        Debug.Log($"You lose.");

        _mainHero.Kill();

        ProcessEndGame();
    }

    private void ProcessEndGame()
    {
        Debug.Log($"Time passed: {_playTimer:F2}. Enemy killed: {_killedEnemies}. Enemy spawned: {_spawnedEnemies}.");

        foreach (var spawner in _enemySpawners)
        {
            spawner.StopSpawnProcess();
            spawner.Spawned -= OnEnemySpawned;
        }

        _isRunning = false;
        _killedEnemies = 0;
        _spawnedEnemies = 0;
        _playTimer = 0;
    }

    private void OnEnemySpawned(AgentEnemyCharacter enemy)
    {
        _spawnedEnemies++;
        _enemies.Add(enemy);
        enemy.KilledBySomeone += OnEnemyKilledByHero;
    }

    private void OnEnemyKilledByHero(IKillable enemy)
    {
        _killedEnemies++;
        _enemies.Remove(enemy as AgentEnemyCharacter);
        enemy.KilledBySomeone -= OnEnemyKilledByHero;
    }

    private void SetWinCondition()
    {
        switch (_levelConfig.WinCondition)
        {
            case WinConditions.SurviveThroughTime:
                WinCondition = () => _playTimer >= _levelConfig.TimeToWin;
                break;

            case WinConditions.KillEnemiesCount:
                WinCondition = () => _killedEnemies >= _levelConfig.KillEnemiesToWin;
                break;
        }
    }

    private void SetDefeatCondition()
    {
        switch (_levelConfig.DefeatCondition)
        {
            case DefeatConditions.MainHeroDied:
                DefeatCondition = () => _mainHero.IsDead;
                break;

            case DefeatConditions.TooMuchEnemies:
                DefeatCondition = () => _enemies.Count >= _levelConfig.MaxSpawnedEnemies;
                break;
        }
    }
}
