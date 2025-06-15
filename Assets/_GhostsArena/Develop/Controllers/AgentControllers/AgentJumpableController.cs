using UnityEngine.AI;

public abstract class AgentJumpableController : Controller
{
    protected AgentCharacter Character;

    public AgentJumpableController(AgentCharacter character)
    {
        Character = character;
    }

    protected override void UpdateLogic()
    {
        if (Character.IsOnNavMeshLink(out OffMeshLinkData offMeshLinkData))
        {
            if(Character.InJumpProcess ==  false)
            {
                Character.SetRotation(offMeshLinkData.endPos - offMeshLinkData.startPos);

                Character.Jump(offMeshLinkData);
            }

            return;
        }

        Character.SetRotation(Character.CurrentVelocity);

        UpdateMovement();
    }

    protected abstract void UpdateMovement();
}
