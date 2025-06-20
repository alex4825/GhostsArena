using System;

public interface IKillable : ITransformPosition
{
    public event Action<IKillable, float> Dead;

    public event Action<IKillable> KilledBySomeone;

    bool IsDead { get; }

    float Lifetime { get; }

    void Kill();
}
