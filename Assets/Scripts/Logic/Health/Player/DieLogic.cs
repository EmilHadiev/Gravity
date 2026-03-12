using UnityEngine;

public abstract class DieLogic : MonoBehaviour
{
    private IHealth _health;
    private void Awake()
    {
        _health = GetComponent<IHealth>();
    }

    private void OnEnable()
    {
        _health.Died += OnDie;
    }

    private void OnDisable()
    {
        _health.Died -= OnDie;
    }

    protected abstract void OnDie();
}
