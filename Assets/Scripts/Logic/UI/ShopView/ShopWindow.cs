using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ShopWindow : MonoBehaviour, IShopWindowStateMachine
{
    [SerializeField] private SwordPurchaseWindow _swordPurchaseWindow;
    [SerializeField] private SkinPurchaseWindow _skinPurchaseWindow;

    [Inject] private readonly ISwordSwitchContainer _swordSwitcher;
    [Inject] private readonly ISkinSwitcherContainer _skinSwitcher;

    private readonly Dictionary<Type, IShopWindowState> _states = new Dictionary<Type, IShopWindowState>();
    private IShopWindowState _currentState;

    private void Start()
    {
        _swordSwitcher.PlayerEntered += OnSwordSwitchContainerEntered;
        _swordSwitcher.PlayerExited += OnPlayerExited;

        _skinSwitcher.PlayerEntered += OnSkinSwitchContainerEntered;
        _skinSwitcher.PlayerExited += OnPlayerExited;

        _states.Add(typeof(SwordPurchaseWindow), _swordPurchaseWindow);
        _states.Add(typeof(SkinPurchaseWindow), _skinPurchaseWindow);
        _states.Add(typeof(EmptyShopState), new EmptyShopState());

        OnPlayerExited();
    }

    private void OnDestroy()
    {
        _swordSwitcher.PlayerEntered -= OnSwordSwitchContainerEntered;
        _swordSwitcher.PlayerExited -= OnPlayerExited;

        _skinSwitcher.PlayerEntered -= OnSkinSwitchContainerEntered;
        _skinSwitcher.PlayerExited -= OnPlayerExited;
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

    private void OnSwordSwitcherContainerEntered()
    {
        throw new NotImplementedException();
    }

    private void OnSwordSwitchContainerEntered(ItemData data)
    {
        _swordPurchaseWindow.SetData(data);
        Switch<SwordPurchaseWindow>();
    }

    private void OnSkinSwitchContainerEntered(ItemData data)
    {
        _skinPurchaseWindow.SetData(data);
        Switch<SkinPurchaseWindow>();
    }

    private void OnPlayerExited()
    {
        Switch<EmptyShopState>();
    }
}
