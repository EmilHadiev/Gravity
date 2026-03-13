using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class EnemyExplosionLogic
{
    private const int Radius = 5;
    private const int MaxTargets = 10;
    private readonly LayerMask _masks;

    private readonly IParticleFactory _factory;
    private readonly IEnemySoundContainer _soundContainer;
    private readonly Transform _enemy;
    private readonly float _damage;

    private readonly Collider[] _targets;

    private ParticleView _particle;

    public EnemyExplosionLogic(IParticleFactory particleFactory, IEnemySoundContainer enemySoundContainer, Transform enemy,
        float damage)
    {
        _factory = particleFactory;
        _soundContainer = enemySoundContainer;
        _enemy = enemy;
        _damage = damage;
        _masks = LayerMask.GetMask(CustmomMasks.Player, CustmomMasks.Enemy);
        _targets = new Collider[MaxTargets];
        CreateParticle().Forget();
    }

    public void BlowUp()
    {
        ClearTargets();

        int countTargers = GetTargetsCount();

        if (countTargers == 0)
            return;

        PlayeView();

        PhysicsDebug.DrawDebug(_enemy.transform.position, Radius, color: Color.yellow);
        for (int i = 0; i < countTargers; i++)
        {
            if (_targets[i].TryGetComponent(out IHealth health))
                health.TakeDamage(_damage);
        }
    }

    private void PlayeView()
    {
        _soundContainer.Play(AssetProvider.Sounds.Explosion.ToString());
        _particle.transform.position = _enemy.transform.position;
        _particle.transform.position += _enemy.transform.up;
        _particle.Play();
    }

    private int GetTargetsCount()
    {
        return Physics.OverlapSphereNonAlloc(_enemy.transform.position, Radius, _targets, _masks);
    }

    private void ClearTargets()
    {
        Array.Clear(_targets, 0, _targets.Length);
    }

    private async UniTaskVoid CreateParticle()
    {
        _particle = await _factory.CreateParticle(AssetProvider.Sounds.Explosion.ToString());
        _particle.Stop();
    }
}