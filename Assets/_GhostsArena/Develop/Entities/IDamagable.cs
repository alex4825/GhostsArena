using System;

public interface IDamagable : ITransformPosition
{
    event Action Hit;
    void TakeDamage(float damage);
}
