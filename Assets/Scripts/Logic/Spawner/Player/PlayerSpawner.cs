using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

public class PlayerSpawner : MonoBehaviour, IPlayerSpawner
{
    public event Action<IPlayer> PlayerSpawned;

    private IFactory _factory;
    private PlayerData _data;

    public IPlayer Player { get; private set; }

    [Inject]
    private void Constructor(IFactory factory, PlayerData playerData)
    {
        _factory = factory;
        _data = playerData;
    }

    public void Spawn(ICameraFollower cameraFollower)
    {
        CreatePlayer(cameraFollower).Forget();
    }

    private async UniTaskVoid CreatePlayer(ICameraFollower cameraFollower)
    {
        var prefab = await _factory.CreateAsync(_data.Player.ToString());
        prefab.transform.position = transform.position;
        prefab.transform.rotation = transform.rotation;

        IPlayer player = prefab.GetComponent<IPlayer>();
        Player = player;

        cameraFollower.SetTarget(prefab.transform);
        PlayerSpawned?.Invoke(Player);
    }
}
