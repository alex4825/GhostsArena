using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private AgentEnemyConfig _agentEnemyConfig;
    [SerializeField] private float _patrolRadius;

    [SerializeField] private float _spawnDuration;

    private CharacterFactory<AgentEnemyCharacter> _enemyFactory;
    private Dictionary<AgentEnemyCharacter, Controller> _enemiesToController = new();

    private IDamagable _target;

    private void Update()
    {
        foreach (var enemyToController in _enemiesToController)
        {
            enemyToController.Value.IsEnabled = enemyToController.Key.IsAlive;
            enemyToController.Value.Update();
        }
    }

    public void Activate(IDamagable target)
    {
        _enemyFactory = new(_agentEnemyConfig.Prefab as AgentEnemyCharacter, transform);

        _target = target;

        StartCoroutine(SpawnProcess());
    }

    private IEnumerator SpawnProcess()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnDuration);

            AgentEnemyCharacter enemy = _enemyFactory.SpawnInRandom(_patrolRadius);
            enemy.Initialize(_agentEnemyConfig);

            yield return new WaitForSeconds(enemy.ShowDuration);

            GenerateControllerFor(enemy);
        }
    }

    private void GenerateControllerFor(AgentEnemyCharacter enemy)
    {
        Controller enemyController = new SwitcherController(
            new AgentEnemyAgroController(enemy, _target),
            new AgentRandomPatrolController(enemy, _patrolRadius));


        _enemiesToController.Add(enemy, enemyController);
        enemyController.IsEnabled = true;

        enemy.Dead += OnEnemyDead;
    }

    private void OnEnemyDead(IKillable enemy, float deadDuration)
    {
        _enemiesToController[enemy as AgentEnemyCharacter].IsEnabled = false;
        _enemiesToController.Remove(enemy as AgentEnemyCharacter);

        enemy.Dead -= OnEnemyDead;
    }
}
