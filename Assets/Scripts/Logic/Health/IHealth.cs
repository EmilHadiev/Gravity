using System;

public interface IHealth : IDamagable
{
    /// <summary>
    /// first - current health
    /// second - max health
    /// </summary>
    event Action<float, float> HealthChanged;

    void AddHealth(float healthPoints);
}