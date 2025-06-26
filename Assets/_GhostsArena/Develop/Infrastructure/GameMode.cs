using System;
using System.Collections.Generic;

public class GameMode
{
    private CharacterControllerCharacter _mainHero;
    private List<EnemiesSpawner> _enemySpawners;

    private List<AgentEnemyCharacter> _enemies = new();

    private bool _isRunning;

    public event Action Win;
    public event Action Defeat;

    private Func<bool> WinCondition;
    private Func<bool> DefeatCondition;

    public GameMode(CharacterControllerCharacter mainHero, List<EnemiesSpawner> enemySpawners)
    {
        _mainHero = mainHero;
        _enemySpawners = enemySpawners;
    }

    public void Start(Func<bool> _winCondition, Func<bool> _defeatCondition)
    {
        WinCondition = _winCondition;
        DefeatCondition = _defeatCondition;

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

        if (WinCondition?.Invoke() == true)
        {
            ProcessEndGame(Win);
            return;
        }

        if (DefeatCondition?.Invoke() == true)
        {
            ProcessEndGame(Defeat);
            return;
        }
    }

    private void ProcessEndGame(Action endGameEvent)
    {
        foreach (var spawner in _enemySpawners)
        {
            spawner.StopSpawnProcess();
            spawner.Spawned -= OnEnemySpawned;
        }

        _isRunning = false;

        for (int i = 0; i < _enemies.Count; i++)
            _enemies[i].Kill();

        _enemies.Clear();

        _mainHero.Kill();

        endGameEvent?.Invoke();
    }

    private void OnEnemySpawned(AgentEnemyCharacter enemy)
    {
        _enemies.Add(enemy);
        enemy.Killed += OnEnemyKilled;
    }

    private void OnEnemyKilled(IKillable enemy)
    {
        _enemies.Remove(enemy as AgentEnemyCharacter);
        enemy.Killed -= OnEnemyKilled;
    }
}
