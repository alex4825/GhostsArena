using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AgentCharacter : Character, IAgentMovable
{
    protected NavMeshAgent Agent;

    private AgentMover _mover;
    private AgentJumper _jumper;

    public override Vector3 CurrentVelocity => _mover.CurrentVelocity;
    public Vector3 CurrentDestination => Agent.destination;
    public override bool InJumpProcess => _jumper.InProcess;

    public void Initialize(AgentConfig config, AgentMover mover, AgentJumper jumper)
    {
        base.Initialize(config);

        Agent = GetComponent<NavMeshAgent>();

        _mover = mover;
        _jumper = jumper;
    }

    public void SetDestination(Vector3 point) => _mover.SetDestination(point);
    
    public bool IsOnNavMeshLink(out OffMeshLinkData offMeshLinkData)
    {
        if (Agent.isOnOffMeshLink)
        {
            offMeshLinkData = Agent.currentOffMeshLinkData;
            return true;
        }

        offMeshLinkData = default;
        return false;
    }

    public void Jump(OffMeshLinkData offMeshLinkData) => _jumper.Jump(offMeshLinkData);
}
