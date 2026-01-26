using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealth
{
    [SerializeField] private float _health;

    public event Action<float, float> HealthChanged;
    public event Action<float> DamageApllied;

    public void AddHealth(float healthPoints)
    {
        if (healthPoints <= 0)
            return;

        _health += healthPoints;
        HealthChanged?.Invoke(_health, 0);
    }

    public void TakeDamage(float damage)
    {
        if (damage <= 0)
            return;

        _health -= damage;

        HealthChanged?.Invoke(_health, 0);
        DamageApllied?.Invoke(damage);

        if (_health <= 0)
            gameObject.SetActive(false);
    }
}