using System;

public interface IKillable : ITransformPosition
{
    public event Action<IKillable> Dead;

    public event Action<IKillable> Killed;

    float DeadDuration { get; }

    bool IsDead { get; }

    float Lifetime { get; }

    void Kill();
}
