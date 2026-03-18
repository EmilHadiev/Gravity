using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ShopWindow : MonoBehaviour, IShopWindowStateMachine
{
    [SerializeField] private PurchaseWindow _purchaseWindow;

    [Inject] private readonly ISwordSwitchContainer _swordSwitcher;

    private readonly Dictionary<Type, IShopWindowState> _states = new Dictionary<Type, IShopWindowState>();
    private IShopWindowState _currentState;

    private void Start()
    {
        _swordSwitcher.PlayerEntered += OnSwordSwitchContainerEntered;
        _swordSwitcher.PlayerExited += OnPlayerExited;

        _states.Add(typeof(PurchaseWindow), _purchaseWindow);
        _states.Add(typeof(EmptyShopState), new EmptyShopState());

        OnPlayerExited();
    }

    private void OnDestroy()
    {
        _swordSwitcher.PlayerEntered -= OnSwordSwitchContainerEntered;
        _swordSwitcher.PlayerExited -= OnPlayerExited;
    }

    public void Switch<T>() where T : IShopWindowState
    {
        if (_states.TryGetValue(typeof(T), out IShopWindowState state))
        {
            _currentState?.Exit();
            _currentState = state;
            _currentState.Enter();
        }
        else
        {
            throw new ArgumentException(nameof(T));
        }
    }

    private void OnSwordSwitchContainerEntered(ItemData data)
    {
        _purchaseWindow.SetData(data);
        Switch<PurchaseWindow>();
    }

    private void OnPlayerExited()
    {
        Switch<EmptyShopState>();
    }
}
