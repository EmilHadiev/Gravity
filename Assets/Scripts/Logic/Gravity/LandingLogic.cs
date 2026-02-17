using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(TriggerObserver))]
public class LandingLogic : MonoBehaviour
{
    [SerializeField] private TriggerObserver _observer;
    [SerializeField] private Vector3 _scale;

    [Inject] private readonly IParticleFactory _factory;

    private bool _isActivated;

    private ParticleView _particle;

    private void OnValidate()
    {
        _observer ??= GetComponent<TriggerObserver>();
    }

    private void OnEnable()
    {
        _observer.Entered += OnGroundEntered;
    }

    private void OnDisable()
    {
        _observer.Entered -= OnGroundEntered;
    }

    private void Start()
    {
        CreateParticle().Forget();
    }

    private async UniTaskVoid CreateParticle()
    {
        _particle = await _factory.CreateParticle(AssetProvider.Particles.GroundHit.ToString());
        _particle.Stop();
        _particle.transform.parent = transform;
        _particle.transform.position = transform.position;
        _particle.transform.localScale = _scale;
    }

    private void OnGroundEntered(Collider collider)
    {
        if (_isActivated == false)
            return;

        if (collider.TryGetComponent(out Ground ground))
            ApplyLanding();
    }

    private void ApplyLanding()
    {
        _particle?.Play();
        Deactivate();
    }

    public void Activate()
    {
        _isActivated = true;
    }

    public void Deactivate()
    {
        _isActivated = false;
    }
}
