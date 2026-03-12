using System;

public interface IHealth : IDamagable
{
    /// <summary>
    /// first - current health
    /// second - max health
    /// </summary>
    event Action<float, float> HealthChanged;
    event Action Died;

    void AddHealth(float healthPoints);
    void Die();
}