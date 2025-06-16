using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerMover
{
    private const float GravityKoef = -9.81f;
    private CharacterController _characterController;

    public CharacterControllerMover(CharacterController characterController)
    {
        _characterController = characterController;
    }

    public void Move(Vector3 velocity)
    {
        _characterController.Move((velocity + Vector3.up * GravityKoef) * Time.deltaTime);
    }
}
