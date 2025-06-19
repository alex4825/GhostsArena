using UnityEngine;

public class AgentEnemyAgroController : AgentJumpableController
{
    private AgentEnemyCharacter _enemyCharacter;
    private IKillable _target;

    public AgentEnemyAgroController(AgentEnemyCharacter character, IKillable target) : base(character)
    {
        _enemyCharacter = character;
        _target = target;
    }

    public override bool HasInput => _target.IsDead == false && Vector3.Distance(_enemyCharacter.transform.position, _target.Position) < _enemyCharacter.AgroRange;

    protected override void UpdateMovement()
    {
        if (Vector3.Distance(_enemyCharacter.transform.position, _target.Position) > _enemyCharacter.MinDistanceToTarget)
            _enemyCharacter.SetDestination(_target.Position);
    }
}
