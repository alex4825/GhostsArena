using UnityEngine;

public interface IShootable
{
    Collider SelfCollider { get; }
    Transform ShootPoint { get; }

    void Shoot();
}
