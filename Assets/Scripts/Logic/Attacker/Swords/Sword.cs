using UnityEngine;

[RequireComponent(typeof(TriggerObserver))]
[RequireComponent(typeof(Rigidbody))]
public class Sword : MonoBehaviour
{
    [SerializeField] private TriggerObserver _observer;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private int _damage = 10;

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

    private void OnEnemyEntered(Collider collider)
    {
        if (collider.TryGetComponent(out IDamagable damagable))
            damagable.TakeDamage(_damage);
    }
}