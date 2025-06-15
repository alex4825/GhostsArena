using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyCharacter : Character, IDirectionalMovable
{
    private float _jumpForce;

    private RigidbodyMover _mover;

    public Vector3 CurrentVelocity => _mover.GetVelocity();

    public void Initialize(MainHeroConfig config)
    {
        base.Initialize(config);
        _jumpForce = config.JumpForce;
        _mover = new RigidbodyMover(GetComponent<Rigidbody>());
    }

    public void SetMovement(Vector3 direction)
        => _mover.Set(direction.normalized * RunSpeed);
}
