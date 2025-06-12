using UnityEngine;

public interface IAgentMovable : IMovable
{
    Vector3 CurrentDestination { get; }

    void SetDestination(Vector3 point);
}
