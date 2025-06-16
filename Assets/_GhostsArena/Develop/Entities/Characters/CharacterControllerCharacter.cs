using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterControllerCharacter : Character, IDirectionalMovable
{
    private CharacterControllerMover _mover;
    private CharacterControllerJumper _jumper;

    private Vector3 _currentVelocity;

    public override Vector3 CurrentVelocity => _currentVelocity;
    public override bool InJumpProcess => _jumper.InProcess;

    public void Initialize(MainHeroConfig config)
    {
        base.Initialize(config);

        _mover = new(GetComponent<CharacterController>());
        _jumper = new(GetComponent<CharacterController>(), this, config.JumpHeight, this);
    }

    public void SetMovement(Vector3 direction)
    {
        _currentVelocity = direction * RunSpeed;
        _mover.Move(CurrentVelocity);
    }

    public void Jump() => _jumper.Jump();
}
