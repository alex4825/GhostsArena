using UnityEngine;

public class AgentRandomPatrolController : AgentJumpableController
{
    private float _patrolRadius;

    private Vector3 _currentDestination;

    public AgentRandomPatrolController(AgentCharacter character, float patrolRadius): base(character)
    {
        _patrolRadius = patrolRadius;
    }

    public override bool HasInput => _currentDestination == Character.CurrentDestination && Character.CurrentVelocity != Vector3.zero;

    protected override void UpdateMovement()
    {
        if (Character.CurrentVelocity == Vector3.zero)
        {
            Character.SetDestination(GetRandomDestinationInRadius());
            _currentDestination = Character.CurrentDestination;
        }
    }

    private Vector3 GetRandomDestinationInRadius()
    {
        float randomAngle = Random.Range(0, 360f);
        float randomDirectionLength = Random.Range(0, _patrolRadius);
        Vector3 moveDirectionNormalized = (Quaternion.Euler(0f, randomAngle, 0f) * Vector3.forward).normalized;

        return moveDirectionNormalized * randomDirectionLength;
    }
}
