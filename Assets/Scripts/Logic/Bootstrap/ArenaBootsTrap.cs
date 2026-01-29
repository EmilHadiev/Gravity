using UnityEngine;
using Zenject;

public class ArenaBootsTrap : MonoBehaviour
{
    [SerializeField] private PlayerSpawner _playerSpawner;
    [SerializeField] private CameraFollower _camera;

    private IPlayerFinder _playerFinder;

    private void Start()
    {
        SpawnPlayer();
    }

    [Inject]
    private void Constructor(IPlayerFinder playerFinder)
    {
        _playerFinder = playerFinder;
    }

    private void SpawnPlayer() => _playerSpawner.Spawn(_camera, _playerFinder);
}
