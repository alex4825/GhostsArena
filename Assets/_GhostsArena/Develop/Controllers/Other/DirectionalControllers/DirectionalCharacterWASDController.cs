using UnityEngine;

public class DirectionalCharacterWASDController : Controller
{
    private const string NameAxisX = "Horizontal";
    private const string NameAxisZ = "Vertical";

    private CharacterControllerCharacter _character;

    public DirectionalCharacterWASDController(CharacterControllerCharacter character)
    {
        _character = character;
    }

    public override bool HasInput => GetInputDirection().magnitude > 0.05f;

    protected override void UpdateLogic()
    {
        Vector3 moveDirection = GetCameraRelativeMovementDirection();

        if (_character.InJumpProcess)
            return;

        if (Input.GetButtonDown("Jump"))
        {
            _character.Jump();
            return;
        }

        _character.SetMovement(moveDirection);
        _character.SetRotation(moveDirection);
    }

    private Vector3 GetInputDirection()
        => new Vector3(Input.GetAxisRaw(NameAxisX), 0, Input.GetAxisRaw(NameAxisZ));

    private Vector3 GetCameraRelativeMovementDirection()
    {
        Vector3 inputDirection = GetInputDirection();

        if (inputDirection.magnitude < 0.05f)
            return Vector3.zero;

        float cameraAngleY = Camera.main.transform.eulerAngles.y;
        Quaternion cameraRotation = Quaternion.Euler(0f, cameraAngleY, 0f);
        Vector3 rotatedDirection = cameraRotation * inputDirection;

        return rotatedDirection.normalized;
    }
}
