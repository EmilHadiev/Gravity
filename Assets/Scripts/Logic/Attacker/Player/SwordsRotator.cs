using UnityEngine;
using Zenject;

public class SwordsRotator : MonoBehaviour, ISwordSwitcher
{
    [SerializeField] private SwordSpawnPoint _spawnPoint;
    [SerializeField] private float _radius = 4f;
    [SerializeField] private float _yPosition = 3f;

    private IFactory _factory;
    private SwordData[] _swordData;

    private Sword _sword;
    private PlayerData _data;

    private SwordCreator _swordCreator;

    private void Awake()
    {
        IPlayer player = GetComponent<IPlayer>();
        _data = player.Data;

        _swordCreator = new SwordCreator(transform, _factory, _spawnPoint, _data, _swordData, _radius, _yPosition);
        CreateSwords();
    }

    private void Update()
    {
        _swordCreator.Rotate();
    }

    public void Switch(AssetProvider.Swords newSword)
    {
        _swordCreator.DestroyAndSetNewSword(newSword);
    }

    private void CreateSwords() => _swordCreator.Create();

    [Inject]
    private void Constructor(IFactory factory, SwordData[] swordData)
    {
        _factory = factory;
        _swordData = swordData;
    }
}