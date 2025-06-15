using UnityEngine;

public class AgentEnemyCharacter : AgentCharacter
{
    public float AgroRange { get; private set; }
    public float MinDistanceToTarget { get; private set; }

    public void Initialize(AgentEnemyConfig config)
    {
        base.Initialize(config);
        AgroRange = config.AgroRange;
        MinDistanceToTarget = config.MinDistanceToTarget;
    }
}
