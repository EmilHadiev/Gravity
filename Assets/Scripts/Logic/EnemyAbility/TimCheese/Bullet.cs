using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(TriggerObserver))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private TriggerObserver _observer;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private int LifeTime = 5;
    [SerializeField] private float BulletDamage = 3f;

    [Inject] private readonly IParticleFactory _factory;

    private CancellationTokenSource _cts;

    private void OnValidate()
    {
        _observer ??= GetComponent<TriggerObserver>();
    }

    private void Awake()
    {
        CreateParticle().Forget();
    }

    private void OnEnable()
    {
        _observer.Entered += OnPlayerEntered;
        HideBullet().Forget();
    }

    private void OnDisable()
    {
        _observer.Entered -= OnPlayerEntered;
    }

    private void Update()
    {
        transform.Translate(transform.forward * _speed * Time.deltaTime, Space.World);
    }

    private void OnDestroy()
    {
        _cts?.Cancel();
        _cts?.Dispose();
    }

    private void OnPlayerEntered(Collider collider)
    {
        if (collider.TryGetComponent(out IHealth health))
        {
            health.TakeDamage(BulletDamage);
            _cts?.Cancel();
            DisableBullet();
        }
    }

    private async UniTaskVoid HideBullet()
    {
        _cts?.Cancel();
        _cts = new CancellationTokenSource();
        await UniTask.Delay(LifeTime * 1000, cancellationToken: _cts.Token);
        DisableBullet();
    }

    private void DisableBullet()
    {
        gameObject.SetActive(false);
    }

    private async UniTaskVoid CreateParticle()
    {
        var particle = await _factory.CreateParticle(AssetProvider.Particles.FireBall.ToString());
        particle.transform.position = transform.position;
        particle.transform.parent = transform;
    }
}
