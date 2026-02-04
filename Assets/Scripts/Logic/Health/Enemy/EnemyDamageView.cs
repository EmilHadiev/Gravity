using UnityEngine;
using Zenject;

[RequireComponent(typeof(CustomGravity))]
public class EnemyDamageView : MonoBehaviour, IKnockable
{
    [SerializeField] private ParticlePlace _impactPlace;
    [SerializeField] private ParticlePlace _damageValuePlace;
    [SerializeField] private CustomGravity _gravity;

    private Knockbacker _knockbacker;
    private DamageImpactViewer _impactViewer;
    private IDamagable _damagable;
    private IParticleFactory _particleFactory;

    private void OnValidate()
    {
        _gravity ??= GetComponent<CustomGravity>();
    }

    private void Awake()
    {
        EnemyData data = GetComponent<IEnemy>().Data;
        _knockbacker = new Knockbacker(transform, _gravity, data);
        _impactViewer = new DamageImpactViewer(_particleFactory, _impactPlace, _damageValuePlace);
        _damagable = GetComponent<IDamagable>();
    }

    private void OnEnable() => _damagable.DamageApllied += OnDamageAppllied;
    private void OnDisable() => _damagable.DamageApllied -= OnDamageAppllied;

    [Inject]
    private void Constructor(IParticleFactory particleFactory)
    {
        _particleFactory = particleFactory;
    }

    public void ApplyKnockBack(float punchDistance)
    {
        _knockbacker.ApplyKnockback(punchDistance);
    }

    private void OnDamageAppllied(float damage)
    {
        _impactViewer.Play(damage);
    }

    private void OnDestroy()
    {
        _knockbacker.KillTweens();
    }
}