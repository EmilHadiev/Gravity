using UnityEngine;

[RequireComponent(typeof(TriggerObserver))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class Sword : MonoBehaviour
{
    [SerializeField] private TriggerObserver _observer;
    [SerializeField] private Rigidbody _rigidbody;

    private SwordData _data;

    private void OnValidate()
    {
        _observer ??= GetComponent<TriggerObserver>();
        _rigidbody ??= GetComponent<Rigidbody>();

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
    }

    private void OnEnemyEntered(Collider collider)
    {
        if (collider.TryGetComponent(out IDamagable damagable))
            damagable.TakeDamage(_data.Damage);

        if (collider.TryGetComponent(out IKnockable knockable))
            knockable.ApplyKnockBack(_data.PushDistance);
    }
}