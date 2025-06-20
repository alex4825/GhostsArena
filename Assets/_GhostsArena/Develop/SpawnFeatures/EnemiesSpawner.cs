using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private AgentEnemyConfig _agentEnemyConfig;
    [SerializeField] private float _patrolRadius;

    [SerializeField] private float _spawnDuration;

    private CharacterFactory<AgentEnemyCharacter> _enemyFactory;
    private ControllersUpdateService _controllersUpdateService;
    private ControllersFactory _controllersFactory;

    private IKillable _target;
    
    public void Initialize(ControllersUpdateService controllersUpdateService, ControllersFactory controllersFactory)
    {
        _controllersUpdateService = controllersUpdateService;
        _controllersFactory = controllersFactory;
    }

    public void Activate(IKillable target)
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

            Controller enemyController = _controllersFactory.CreateEnemyController(enemy, _target, _patrolRadius);

            _controllersUpdateService.Add(enemy, enemyController);
        }
    }
}
