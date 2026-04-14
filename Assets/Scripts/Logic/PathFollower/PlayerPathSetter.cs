using UnityEngine;
using Zenject;

public class PlayerPathSetter : MonoBehaviour
{
    [Inject] private readonly IPlayerSpawner _playerSpawner;

    private void OnEnable()
    {
        _playerSpawner.PlayerSpawned += OnPlayerSpawned;
    }

    private void OnDisable()
    {
        _playerSpawner.PlayerSpawned -= OnPlayerSpawned;
    }

    private void OnPlayerSpawned(IPlayer player)
    {
        player.Follower.SetTarget(transform);
    }
}