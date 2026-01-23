using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class PlayerSpawner : MonoBehaviour
{
    [Inject] private readonly IFactory _factory;

    public void Spawn(ICameraFollower cameraFollower)
    {
        CreatePlayer(cameraFollower).Forget();
    }

    private async UniTask CreatePlayer(ICameraFollower cameraFollower)
    {
        var prefab = await _factory.CreateAsync(AssetProvider.Player);
        prefab.transform.position = transform.position;
        prefab.transform.rotation = transform.rotation;

        cameraFollower.SetTarget(prefab.transform);
    }
}
