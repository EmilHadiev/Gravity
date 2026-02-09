using System;

public interface IHealth : IDamagable
{
    /// <summary>
    /// first - current health
    /// second - max health
    /// </summary>
    event Action<float, float> HealthChanged;
    event Action Die;

    void AddHealth(float healthPoints);
}