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
            Character.SetDestination(RandomPointer.GetRandomPointInRadius(_patrolRadius));
            _currentDestination = Character.CurrentDestination;
        }
    }
}
