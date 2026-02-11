using System;
using UnityEngine;
using Zenject;

public class ArenaBootsTrap : MonoBehaviour
{
    [SerializeField] private PlayerSpawner _playerSpawner;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private CameraFollower _camera;

    private void OnEnable()
    {
        _playerSpawner.PlayerSpawned += OnPlayerSpawned;
    }

    private void OnDisable()
    {
        _playerSpawner.PlayerSpawned += OnPlayerSpawned;
    }

    private void Start()
    {
        SpawnPlayer();
    }
    private void SpawnPlayer()
    {
        _playerSpawner.Spawn(_camera);
    }

    private void SpawnEnemy()
    {
        _enemySpawner.Spawn();
    }

    private void OnPlayerSpawned()
    {
        SpawnEnemy();
    }
}
