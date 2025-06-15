using UnityEngine;

public interface IDirectionalMovable : IMovable
{
    void SetMovement(Vector3 direction);
}
