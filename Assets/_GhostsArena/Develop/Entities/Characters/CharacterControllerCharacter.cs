using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterControllerCharacter : Character, IDirectionalMovable
{
    private float _jumpForce;
    private CharacterControllerMover _mover;

    public Vector3 CurrentVelocity { get; private set; }

    public void Initialize(MainHeroConfig config)
    {
        base.Initialize(config);
        _jumpForce = config.JumpForce;
        _mover = new(GetComponent<CharacterController>());
    }
     
    public void SetMovement(Vector3 direction)
    {
        CurrentVelocity = direction * RunSpeed;
        _mover.Move(CurrentVelocity);
    }
}
