using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    private PlayerData _data;
    private float _maxHealth;
    private float _currentHealth;

    public event Action<float, float> HealthChanged;
    public event Action<float> DamageApllied;

    private void Awake()
    {
        IPlayer player = GetComponent<IPlayer>();
        _data = player.Data;
        _maxHealth = _data.Health;
        _currentHealth = _data.Health;
    }

    public void AddHealth(float healthPoints)
    {
        _currentHealth += healthPoints;

        if (_currentHealth >= _maxHealth)
            _currentHealth = _maxHealth;

        HealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
            Die();

        HealthChanged?.Invoke(_currentHealth, _maxHealth);
        DamageApllied?.Invoke(damage);

        Debug.Log(_currentHealth);
    }

    private void Die()
    {
        Debug.Log("Игрок все...");
        gameObject.SetActive(false);
    }
}