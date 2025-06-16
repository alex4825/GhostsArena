using UnityEngine;

public class AgentEnemyAgroController : AgentJumpableController
{
    private AgentEnemyCharacter _enemyCharacter;
    private Transform _target;

    public AgentEnemyAgroController(AgentEnemyCharacter character, Transform target) : base(character)
    {
        _enemyCharacter = character;
        _target = target;
    }

    public override bool HasInput => GetDistanceBetween(_enemyCharacter.transform, _target) < _enemyCharacter.AgroRange;

    protected override void UpdateMovement()
    {
        if (GetDistanceBetween(_enemyCharacter.transform, _target) > _enemyCharacter.MinDistanceToTarget)
            _enemyCharacter.SetDestination(_target.position);
    }

    private float GetDistanceBetween(Transform target1, Transform target2)
        => Vector3.Distance(target1.position, target2.position);
}
