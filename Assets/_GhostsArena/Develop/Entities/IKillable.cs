using System;

public interface IKillable : ITransformPosition
{
    public event Action<IKillable, float> Dead;

    bool IsDead { get; }

    float Lifetime { get; }

    void Kill();
}
