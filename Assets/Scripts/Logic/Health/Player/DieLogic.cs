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
        _health.Die += OnDie;
    }

    private void OnDisable()
    {
        _health.Die -= OnDie;
    }

    protected abstract void OnDie();
}
