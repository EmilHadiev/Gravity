using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private AssetProvider.Enemies _enemy;
    [SerializeField] private EnemySpawnPosition[] _spawnPositions;

    [Inject] private readonly IFactory _factory;

    private async UniTaskVoid SpawnEnemy()
    {
        for (int i = 0; i < _spawnPositions.Length; i++)
        {
            await UniTask.NextFrame();
            var prefab = await _factory.CreateAsync(_enemy.ToString());
            prefab.transform.SetPositionAndRotation(_spawnPositions[i].transform.position, _spawnPositions[i].transform.rotation);
        }
    }

    public void Spawn()
    {
        SpawnEnemy().Forget();
    }
}