using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterControllerCharacter : Character, IDirectionalMovable
{
    private CharacterControllerMover _mover;
    private CharacterControllerJumper _jumper;

    public Vector3 CurrentVelocity { get; private set; }
    public bool InJumpProcess => _jumper.InProcess;

    public void Initialize(MainHeroConfig config)
    {
        base.Initialize(config);

        _mover = new(GetComponent<CharacterController>());
        _jumper = new(GetComponent<CharacterController>(), this, config.JumpHeight, this);
    }

    public void SetMovement(Vector3 direction)
    {
        CurrentVelocity = direction * RunSpeed;
        _mover.Move(CurrentVelocity);
    }

    public void Jump() => _jumper.Jump();
}
