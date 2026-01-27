using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealth
{
    private EnemyData _enemyData;

    private float _currentHealth;
    private float _maxHealth;

    public event Action<float, float> HealthChanged;
    public event Action<float> DamageApllied;

    private void Awake()
    {
        IEnemy enemy = GetComponent<Enemy>();
        _enemyData = enemy.Data;

        _currentHealth = _enemyData.Health;
        _maxHealth = _enemyData.Health;
    }

    public void AddHealth(float healthPoints)
    {
        if (healthPoints <= 0)
            return;

        _currentHealth += healthPoints;

        if (_currentHealth > _maxHealth)
            _currentHealth = _maxHealth;

        HealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    public void TakeDamage(float damage)
    {
        if (damage <= 0)
            return;

        _currentHealth -= damage;

        HealthChanged?.Invoke(_currentHealth, _maxHealth);
        DamageApllied?.Invoke(damage);

        if (_currentHealth <= 0)
            gameObject.SetActive(false);
    }
}