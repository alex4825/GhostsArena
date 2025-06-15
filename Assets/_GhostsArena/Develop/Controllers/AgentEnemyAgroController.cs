using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class AgentEnemyAgroController : AgentJumpableController
{
    private AgentEnemyCharacter _enemyCharacter;
    private Transform _target;

    public AgentEnemyAgroController(AgentEnemyCharacter character, Transform target): base(character)
    {
        _enemyCharacter = character;
        _target = target;
    }

    public override bool HasInput => GetDistanceBetween(_enemyCharacter.transform, _target) < _enemyCharacter.AgroRange;

    protected override void UpdateMovement()
    {
        _enemyCharacter.SetDestination(_target.position);
    }
    
    private float GetDistanceBetween(Transform target1 , Transform target2)
        => Vector3.Distance(target1.position, target2.position);
}
