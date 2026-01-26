using System;

public interface IDamagable
{
    event Action<float> DamageApllied;

    void TakeDamage(float damage);
}