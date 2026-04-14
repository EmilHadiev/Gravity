using UnityEngine;
using Zenject;

public class ArenaBootsTrap : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private CameraFollower _camera;

    [Inject] private readonly IPlayerSpawner _playerSpawner;

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

    private void OnPlayerSpawned(IPlayer player)
    {
        SpawnEnemy();
    }
}
