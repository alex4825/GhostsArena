using System.Collections;
using UnityEngine;

public class CharacterControllerJumper
{
    private const float GravityKoef = -9.81f;

    private CharacterController _characterController;
    private IMovable _movable;

    private Coroutine _jumpProcess;
    private MonoBehaviour _coroutineRunner;

    private Vector3 _jumpForceY;

    public CharacterControllerJumper(CharacterController characterController, IMovable movable, float jumpHeight, MonoBehaviour coroutineRunner)
    {
        _characterController = characterController;
        _movable = movable;
        _coroutineRunner = coroutineRunner;

        _jumpForceY = Vector3.up * Mathf.Sqrt(jumpHeight * -2f * GravityKoef);
    }

    public bool InProcess => _jumpProcess != null;

    public void Jump()
    {
        if (InProcess)
            return;

        _jumpProcess = _coroutineRunner.StartCoroutine(JumpProcess());
    }

    private IEnumerator JumpProcess()
    {
        Vector3 verticalDirection = _jumpForceY;
        bool isJump = true;

        while (isJump)
        {
            verticalDirection += Vector3.up * GravityKoef * Time.deltaTime;
            _characterController.Move((_movable.CurrentVelocity + verticalDirection) * Time.deltaTime);

            if (_characterController.isGrounded)
                isJump = false;

            yield return null;
        }

        _jumpProcess = null;
    }
}
