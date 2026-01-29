using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class PlayerSpawner : MonoBehaviour
{
    [Inject] private readonly IFactory _factory;
    [SerializeField] private AssetProvider.Player _player;

    public void Spawn(ICameraFollower cameraFollower, IPlayerFinder playerFinder)
    {
        CreatePlayer(cameraFollower, playerFinder).Forget();
    }

    private async UniTask CreatePlayer(ICameraFollower cameraFollower, IPlayerFinder playerFinder)
    {
        var prefab = await _factory.CreateAsync(_player.ToString());
        prefab.transform.position = transform.position;
        prefab.transform.rotation = transform.rotation;

        cameraFollower.SetTarget(prefab.transform);
        playerFinder.SetPlayer(prefab.GetComponent<IPlayer>());
    }
}
