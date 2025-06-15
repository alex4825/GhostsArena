using UnityEngine;

public class AgentClickPointController : AgentJumpableController
{
    private const int LeftMouseKey = 0;

    private LayerMask _groundMask;

    private Vector3 _currentDestination;

    public AgentClickPointController(AgentCharacter character, LayerMask groundMask) : base(character)
    {
        _groundMask = groundMask;
    }

    public override bool HasInput => _currentDestination == Character.CurrentDestination && Character.CurrentVelocity != Vector3.zero;

    protected override void UpdateMovement()
    {
        if (TryGetClickPosition(out Vector3 clickPosition))
        {
            Character.SetDestination(clickPosition);
            _currentDestination = Character.CurrentDestination;
        }
    }

    private bool TryGetClickPosition(out Vector3 clickPosition)
    {
        if (Input.GetMouseButtonDown(LeftMouseKey))
        {
            Ray cursorRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(cursorRay, out RaycastHit hit, Mathf.Infinity, _groundMask))
            {
                clickPosition = hit.point;
                return true;
            }
        }

        clickPosition = Vector3.zero;
        return false;
    }
}
