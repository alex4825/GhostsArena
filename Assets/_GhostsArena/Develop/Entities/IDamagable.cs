using System;

public interface IDamagable : ITransformPosition
{
    float MaxHealth { get; }
    float Health { get; }

    event Action Hit;
    void TakeDamage(float damage);
}
