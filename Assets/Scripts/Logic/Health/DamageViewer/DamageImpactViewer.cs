using Cysharp.Threading.Tasks;
using UnityEngine;

public class DamageImpactViewer
{
    private readonly IParticleFactory _particleFactory;
    private readonly Transform _damageImpactPlace;
    private readonly Transform _damageValuePlace;

    private ParticleView _damageImpact;
    private ParticleViewText _damageValue;

    public DamageImpactViewer(IParticleFactory factory, 
        ParticlePlace damageImpact, ParticlePlace damageValue)
    {
        _particleFactory = factory;
        _damageImpactPlace = damageImpact.transform;
        _damageValuePlace = damageValue.transform;

        CreateParticles().Forget();
    }

    public void Play(float damage)
    {
        _damageImpact.Play();

        _damageValue.ChangeText(damage.ToString());
        _damageValue.Play();
    }

    private async UniTaskVoid CreateParticles()
    {
        _damageImpact = await _particleFactory.CreateParticle(AssetProvider.ParticleDamageImpact);
        _damageImpact.Stop();
        SetPosition(_damageImpact, _damageImpactPlace);

        _damageValue = await _particleFactory.CreateParticleText(AssetProvider.PartcleDamageText);
        _damageValue.Stop();
        SetPosition(_damageValue, _damageValuePlace);
    }

    private void SetPosition(ParticleView view, Transform particlePlace)
    {
        view.transform.parent = particlePlace;
        view.transform.position = particlePlace.position;
        view.transform.rotation = particlePlace.rotation;
    }
}