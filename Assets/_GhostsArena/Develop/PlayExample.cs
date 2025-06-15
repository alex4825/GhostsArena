using UnityEngine;

public class PlayExample : MonoBehaviour
{
    [SerializeField] private AgentConfig _agentHeroConfig;
    [SerializeField] private AgentEnemyConfig _agentEnemyConfig;
    [SerializeField] private Transform _heroSpawnPoint;
    [SerializeField] private Transform _enemySpawnPoint;
    [SerializeField] private LayerMask _groundMask;

    [SerializeField] private float _patrolRadius;

    private AgentEnemyCharacter _enemyCharacter;
    private AgentCharacter _heroCharacter;

    private Controller _agentEnemyController;
    //private Controller _agentHeroController;

    private void Awake()
    {
        _enemyCharacter = Instantiate(_agentEnemyConfig.Prefab as AgentEnemyCharacter, _enemySpawnPoint.position, Quaternion.identity);
        _enemyCharacter.Initialize(_agentEnemyConfig);

        _heroCharacter = Instantiate(_agentHeroConfig.Prefab as AgentCharacter, _heroSpawnPoint.position, Quaternion.identity);
        _heroCharacter.Initialize(_agentHeroConfig);

        _agentEnemyController = new SwitcherController(
            new AgentEnemyAgroController(_enemyCharacter, _heroCharacter.transform),
            new AgentRandomPatrolController(_enemyCharacter, _patrolRadius)); 

        _agentEnemyController.IsEnabled = true;
    }

    private void Update()
    {
        _agentEnemyController.IsEnabled = _enemyCharacter.IsAlive;

        _agentEnemyController.Update();
    }

}
