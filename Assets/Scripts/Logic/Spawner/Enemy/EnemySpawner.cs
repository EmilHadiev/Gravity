using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private AssetProvider.Enemies _enemy;
    [SerializeField] private EnemySpawnPosition _spawnPosition;

    [Inject] private readonly IFactory _factory;

    private void Start()
    {
        SpawnEnemy().Forget();
    }

    private async UniTaskVoid SpawnEnemy()
    {
        var prefab = await _factory.CreateAsync(_enemy.ToString());
        prefab.transform.SetPositionAndRotation(_spawnPosition.transform.position, _spawnPosition.transform.rotation);
    }
}