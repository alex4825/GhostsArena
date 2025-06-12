using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayExample : MonoBehaviour
{
    [SerializeField] private AgentConfig _agentConfig;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private LayerMask _groundMask;

    [SerializeField] private float _maxIdleTime;
    [SerializeField] private float _patrolRadius;

    private AgentCharacter _character;
    private Controller _agentCharacterController;

    private void Awake()
    {
        _character = Instantiate(_agentConfig.Prefab, _spawnPoint.position, Quaternion.identity);
        _character.Initialize(_agentConfig);

        _agentCharacterController = new CompositeController(
            new AgentClickPointController(_character, _groundMask),
            new AgentRandomPatrolController(_character, _patrolRadius),
            _maxIdleTime); 

        _agentCharacterController.IsEnabled = true;
    }

    private void Update()
    {
        _agentCharacterController.IsEnabled = _character.IsAlive;

        _agentCharacterController.Update();
    }

}
