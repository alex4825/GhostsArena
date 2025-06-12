using System;

public interface IKillable
{
    public event Action<IKillable, float> Dead;

    bool IsDead { get; }

    float Lifetime { get; }

    void Kill();
}
