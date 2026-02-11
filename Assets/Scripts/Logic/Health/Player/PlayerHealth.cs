using System;
using UnityEngine;
using Zenject;

public class PlayerHealth : MonoBehaviour, IHealth
{
    private PlayerData _data;
    private float _maxHealth;
    private float _currentHealth;

    [Inject] private readonly IPlayerSoundContainer _playerSound;

    public event Action<float, float> HealthChanged;
    public event Action<float> DamageApllied;
    public event Action Die;

    private void Awake()
    {
        IPlayer player = GetComponent<IPlayer>();
        _data = player.Data;
        _maxHealth = _data.Health;
        _currentHealth = _data.Health;
    }

    private void Start()
    {
        HealthChanged?.Invoke(_currentHealth, _maxHealth);
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
        {
            Die?.Invoke();
        }
        else
        {
            HealthChanged?.Invoke(_currentHealth, _maxHealth);
            DamageApllied?.Invoke(damage);
            _playerSound.PlayWhenFree(AssetProvider.Sounds.PlayerTakeDamage.ToString());
        } 

        Debug.Log(_currentHealth);
    }
}