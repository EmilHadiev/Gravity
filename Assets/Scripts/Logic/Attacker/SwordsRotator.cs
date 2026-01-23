using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class SwordsRotator : MonoBehaviour
{
    [SerializeField] private SwordSpawnPoint _spawnPoint;

    [Inject] private readonly IFactory _factory;

    private Sword _sword;
    private PlayerData _data;

    private void Awake()
    {
        IPlayer player = GetComponent<IPlayer>();
        _data = player.Data;

        CreateSwords().Forget();
    }

    private async UniTask CreateSwords()
    {
        var prefab = await _factory.CreateAsync(_data.Swords.ToString());
        prefab.transform.parent = _spawnPoint.transform;
        prefab.transform.position = _spawnPoint.transform.position;

        _sword = prefab.GetComponent<Sword>();
    }

    private void Update()
    {
        if (_sword != null)
        {
            RotateObject(_spawnPoint.transform);
        }
    }

    private void RotateObject(Transform obj)
    {
        obj.RotateAround(transform.position, Vector3.up, _data.AttackSpeed * Time.deltaTime);
    }
}