using UnityEngine;

public class AgentEnemyCharacter : AgentCharacter
{
    private float _attackCooldown;
    private float _attackTimer;

    public float AgroRange { get; private set; }
    public float MinDistanceToTarget { get; private set; }

    public void Initialize(AgentEnemyConfig config, AgentMover mover, AgentJumper jumper)
    {
        base.Initialize(config, mover, jumper);
        _attackCooldown = config.AttackCooldown;
        _attackTimer = 0;

        AgroRange = config.AgroRange;
        MinDistanceToTarget = config.MinDistanceToTarget;
        Agent.stoppingDistance = MinDistanceToTarget;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Character collidedCharacter) == false)
            return;

        if (collidedCharacter.Race == Race)
            return;

        if (_attackTimer == 0)
            collidedCharacter.TakeDamage(MeleeAttack);

        if (_attackTimer <= _attackCooldown)
            _attackTimer += Time.deltaTime;
        else
            _attackTimer = 0;
    }
}
