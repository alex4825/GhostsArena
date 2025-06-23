using System;
using System.Collections;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private AgentEnemyConfig _agentEnemyConfig;
    [SerializeField] private float _spawnRadius;

    [SerializeField] private float _spawnDuration;

    private CharactersFactory _charactersFactory;
    private ControllersUpdateService _controllersUpdateService;
    private ControllersFactory _controllersFactory;

    private IKillable _target;
    private Coroutine _spawnProcess;

    public Action<AgentEnemyCharacter> Spawned;

    public void Initialize(
        ControllersUpdateService controllersUpdateService,
        ControllersFactory controllersFactory,
        CharactersFactory charactersFactory)
    {
        _controllersUpdateService = controllersUpdateService;
        _controllersFactory = controllersFactory;
        _charactersFactory = charactersFactory;
    }

    public void ActivateSpawnProcess(IKillable target)
    {
        _target = target;

        _spawnProcess = StartCoroutine(SpawnProcess());
    }

    public void StopSpawnProcess()
    {
        _target = null;
        StopCoroutine(_spawnProcess);
    }

    private IEnumerator SpawnProcess()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnDuration);

            AgentEnemyCharacter enemy = _charactersFactory.CreateEnemyCharacter(_agentEnemyConfig, GetRandomSpawnPosition());

            Spawned?.Invoke(enemy);

            yield return new WaitForSeconds(enemy.ShowDuration);

            Controller enemyController = _controllersFactory.CreateEnemyController(enemy, _target, _agentEnemyConfig.PatrolRadius);

            _controllersUpdateService.Add(enemy, enemyController);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Collider enemyCollider = _agentEnemyConfig.Prefab.GetComponent<Collider>();

        float maxColliderRadius = enemyCollider.bounds.extents.magnitude;
        int maxAttempts = 10;
        bool hasIntersections;
        int attempt = 0;
        Vector3 offset;

        do
        {
            attempt++;
            offset = RandomPointer.GetRandomPointInRadius(_spawnRadius);
            hasIntersections = Physics.CheckSphere(transform.position + offset + enemyCollider.bounds.center, maxColliderRadius);
        }
        while (hasIntersections || attempt < maxAttempts);

        return transform.position + offset;
    }
}
