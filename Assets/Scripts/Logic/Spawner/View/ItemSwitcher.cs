using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(TriggerObserver))]
[RequireComponent(typeof(MaterialColorChanger))]
public abstract class ItemSwitcher : MonoBehaviour
{
    [SerializeField] private TriggerObserver _observer;
    [SerializeField] private MaterialColorChanger _colorChanger;

    [Inject] protected readonly IFactory Factory;

    protected ItemData Data;

    public event Action<ItemData> PlayerEntered;
    public event Action PlayerExited;

    private void OnValidate()
    {
        _observer ??= GetComponent<TriggerObserver>();
        _colorChanger ??= GetComponent<MaterialColorChanger>();
    }

    private void OnEnable()
    {
        _observer.Entered += OnPlayerEntered;
        _observer.Exited += OnPlayerExited;
    }

    private void OnDisable()
    {
        _observer.Entered -= OnPlayerEntered;
        _observer.Exited -= OnPlayerExited;
    }

    public virtual void SetData(ItemData data)
    {
        Data = data;
        ChangeColor();
        CreateView();
    }

    protected abstract void CreateView();

    protected void ChangeColor()
    {
        if (Data.IsPurchase)
        {
            _colorChanger.SetColor(color: _colorChanger.Gold);
        }
    }

    protected void OnPlayerEntered(Collider collider)
    {
        TrySetOptionToPlayerComponent(collider);
        PlayerEntered?.Invoke(Data);
    }
    protected void OnPlayerExited(Collider collider) => PlayerExited?.Invoke();

    protected virtual void TrySetOptionToPlayerComponent(Collider collider)
    {

    }
}
