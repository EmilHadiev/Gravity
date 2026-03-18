using System;
using UnityEngine;

[RequireComponent(typeof(TriggerObserver))]
[RequireComponent(typeof(SwitcherView))]
[RequireComponent(typeof(MaterialColorChanger))]
public class SwordSwitcher : MonoBehaviour
{
    [SerializeField] private TriggerObserver _observer;
    [SerializeField] private SwitcherView _view;
    [SerializeField] private SwordInfoView _swordInfoView;
    [SerializeField] private MaterialColorChanger _colorChanger;

    private SwordData _data;
    private ISwordSwitcher _switcher;

    public event Action<ItemData> PlayerEntered;
    public event Action PlayerExited;

    private void OnValidate()
    {
        _observer ??= GetComponent<TriggerObserver>();
        _view ??= GetComponent<SwitcherView>();
        _swordInfoView ??= GetComponentInChildren<SwordInfoView>();
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

    public void ShowSwordInfo(SwordData swordData)
    {
        _data = swordData;
        _swordInfoView.SetData(_data);

        _view.CreateSwordView(_data.Sword);
        TrySetPurchaseColor();
    }

    public bool TryChangeSword(string swordName)
    {
        bool result = _data.Sword.ToString() == swordName;

        if (result)
        {
            _switcher.Switch(_data.Sword);
            TrySetPurchaseColor();
        }

        return result;
    }

    private void OnPlayerEntered(Collider collider)
    {
        if (collider.TryGetComponent(out ISwordSwitcher switcher))
        {
            _switcher = switcher;
            PlayerEntered?.Invoke(_data);
        }   
    }

    private void OnPlayerExited(Collider collider)
    {
        PlayerExited?.Invoke();
    }

    private void TrySetPurchaseColor()
    {
        if (_data.IsPurchase)
        {
            Color gold = new Color(1, 0.8392157f, 0, 1);
            _colorChanger.SetColor(color: gold);
        }        
    }
}