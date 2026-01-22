using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private PlayerSpawnPoint _spawnPoint;

    [Inject] private readonly IFactory _factory;

    public void Spawn(ICameraFollower cameraFollower)
    {
        CreatePlayer(cameraFollower).Forget();
    }

    private async UniTask CreatePlayer(ICameraFollower cameraFollower)
    {
        var prefab = await _factory.CreateAsync(AssetProvider.Player);
        prefab.transform.position = _spawnPoint.transform.position;
        prefab.transform.rotation = _spawnPoint.transform.rotation;

        cameraFollower.SetTarget(prefab.transform);
    }
}
