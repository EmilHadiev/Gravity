using UnityEngine;
using Zenject;

public class TimCheeseAbility : EnemyAbilityActivator
{
    [SerializeField] private Transform _bulletPlace;
    [SerializeField] private Bullet _prefab;

    [Inject] private readonly IFactory _factory;
    [Inject] private readonly IEnemySoundContainer _sound;

    private BulletStorage _bulletStorage;

    private void Awake()
    {
        _bulletStorage = new BulletStorage(_factory, _prefab);
    }

    private void Attacked()
    {
        Activate();
    }

    public override void Activate()
    {
        if (_bulletStorage.GetBullet(out Bullet bullet))
        {
            bullet.transform.SetPositionAndRotation(_bulletPlace.position, _bulletPlace.rotation);
            bullet.gameObject.SetActive(true);
            _sound.Play(AssetProvider.Sounds.Shooting.ToString());
        }
    }
}