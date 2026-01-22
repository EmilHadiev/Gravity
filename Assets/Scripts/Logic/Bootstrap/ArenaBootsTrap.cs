using UnityEngine;

public class ArenaBootsTrap : MonoBehaviour
{
    [SerializeField] private PlayerSpawner _playerSpawner;
    [SerializeField] private CameraFollower _camera;

    private void Start()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer() => _playerSpawner.Spawn(_camera);
}
