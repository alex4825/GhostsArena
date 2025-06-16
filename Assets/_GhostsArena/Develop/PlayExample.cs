using Cinemachine;
using UnityEngine;

public class PlayExample : MonoBehaviour
{
    [SerializeField] private MainHeroConfig _mainHeroConfig;
    [SerializeField] private AgentEnemyConfig _agentEnemyConfig;

    [SerializeField] private Transform _heroSpawnPoint;
    [SerializeField] private Transform _enemySpawnPoint;

    [SerializeField] private LayerMask _groundMask;

    [SerializeField] private float _patrolRadius;

    private AgentEnemyCharacter _enemyCharacter;
    private CharacterControllerCharacter _heroCharacter;

    private Controller _agentEnemyController;
    private Controller _heroController;

    private CinemachineVirtualCamera _mainHeroFollowCamera;

    private void Awake()
    {
        _enemyCharacter = Instantiate(_agentEnemyConfig.Prefab as AgentEnemyCharacter, _enemySpawnPoint.position, Quaternion.identity);
        _enemyCharacter.Initialize(_agentEnemyConfig);

        _heroCharacter = Instantiate(_mainHeroConfig.Prefab as CharacterControllerCharacter, _heroSpawnPoint.position, Quaternion.identity);
        _heroCharacter.Initialize(_mainHeroConfig);

        _agentEnemyController = new SwitcherController(
            new AgentEnemyAgroController(_enemyCharacter, _heroCharacter.transform),
            new AgentRandomPatrolController(_enemyCharacter, _patrolRadius));

        _heroController = new DirectionalCharacterControllerWASDController(_heroCharacter);

        _agentEnemyController.IsEnabled = true;
        _heroController.IsEnabled = true;

        _mainHeroFollowCamera = Instantiate(_mainHeroConfig.FollowCameraPrefab);
        _mainHeroFollowCamera.Follow = _heroCharacter.transform;
        _mainHeroFollowCamera.LookAt = _heroCharacter.transform;
    }

    private void Update()
    {
        _agentEnemyController.IsEnabled = _enemyCharacter.IsAlive;
        _heroController.IsEnabled = _heroCharacter.IsAlive;

        _agentEnemyController.Update();
        _heroController.Update();
    }
}
