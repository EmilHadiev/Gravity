using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class SwordsRotator : MonoBehaviour
{
    [SerializeField] private AssetProvider.Swords _swords;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private SwordSpawnPoint _spawnPoint;

    [Inject] private readonly IFactory _factory;

    private Sword _sword;


    private void Awake()
    {
        CreateSwords().Forget();
    }

    private async UniTask CreateSwords()
    {
        var prefab = await _factory.CreateAsync(_swords.ToString());
        prefab.transform.parent = transform;
        prefab.transform.position = _spawnPoint.transform.position;

        _sword = prefab.GetComponent<Sword>();
    }

    private void Update()
    {
        if (_sword != null)
            _sword.transform.RotateAround(transform.position, Vector3.up, _attackSpeed * Time.deltaTime);
    }
}