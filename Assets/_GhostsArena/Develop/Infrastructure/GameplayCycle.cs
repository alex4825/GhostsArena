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
    }

    public IEnumerator Launch()
    {
        yield return _context.StartCoroutine(_mainHeroSpawner.Spawn(hero => _mainHero = hero, _mainHeroSpawnPoint.position));

        _confirmPopup.Show();
        _confirmPopup.ShowMessage($"Press {KeyCode.F.ToString()} for begin");

        yield return _confirmPopup.WaitConfirm(KeyCode.F);

        _confirmPopup.Hide();

        _gameMode = new(_levelConfig, _mainHero, _enemiesSpawners);

        _gameMode.Win += OnGameModeWin;
        _gameMode.Defeat += OnGameModeDefeat;

        _gameMode.Start();
    }

    public void Update() => _gameMode?.Update();

    public void Dispose()
    {
        OnGameModeEnded();
    }

    private void OnGameModeEnded()
    {
        if (_gameMode != null)
        {
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
}
