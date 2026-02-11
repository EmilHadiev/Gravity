using UnityEngine;
using Zenject;

[RequireComponent(typeof(TriggerObserver))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(SwordTrial))]
public class Sword : MonoBehaviour
{
    [SerializeField] private TriggerObserver _observer;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private SwordTrial _swordTrial;

    [Inject] private readonly IPlayerSoundContainer _playerSound;

    private SwordData _data;

    private void OnValidate()
    {
        _observer ??= GetComponent<TriggerObserver>();
        _rigidbody ??= GetComponent<Rigidbody>();
        _swordTrial ??= GetComponent<SwordTrial>();

        if (_rigidbody.isKinematic == false)
            _rigidbody.isKinematic = true;
    }

    private void OnEnable()
    {
        _observer.Entered += OnEnemyEntered;
    }

    private void OnDisable()
    {
        _observer.Entered -= OnEnemyEntered;
    }

    public void SetData(SwordData swordData)
    {
        _data = swordData;
        _swordTrial.SetColor(swordData.Color);
    }

    private void OnEnemyEntered(Collider collider)
    {
        if (collider.TryGetComponent(out IDamagable damagable))
        {
            damagable.TakeDamage(_data.Damage);
            _playerSound.Play(AssetProvider.Sounds.Attack.ToString());
        }

        if (collider.TryGetComponent(out IKnockable knockable))
            knockable.ApplyKnockBack(_data.PushDistance);
    }
}