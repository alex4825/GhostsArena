using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Gameplay/AgentEnemyConfig", fileName = "AgentEnemyConfig")]
public class AgentEnemyConfig : AgentConfig
{
    [field: SerializeField] public AgentEnemyCharacter AgentEnemyCharacterPrefab { get; private set; }
    [field: SerializeField] public float PatrolRadius { get; private set; } = 10f;
    [field: SerializeField] public float AgroRange { get; private set; } = 10f;
    [field: SerializeField] public float MinDistanceToTarget { get; private set; } = 10f;
    [field: SerializeField] public float AttackCooldown { get; private set; } = 1f;
}
