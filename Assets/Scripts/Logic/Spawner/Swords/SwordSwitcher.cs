using System;
using UnityEngine;

[RequireComponent(typeof(TriggerObserver))]
[RequireComponent(typeof(SwitcherView))]
public class SwordSwitcher : MonoBehaviour
{
    [SerializeField] private TriggerObserver _observer;
    [SerializeField] private SwitcherView _view;
    [SerializeField] private SwordInfoView _swordInfoView;

    private SwordData _data;
    private ISwordSwitcher _switcher;

    public event Action<ItemData> PlayerEntered;
    public event Action PlayerExited;

    private void OnValidate()
    {
        _observer ??= GetComponent<TriggerObserver>();
        _view ??= GetComponent<SwitcherView>();
        _swordInfoView ??= GetComponentInChildren<SwordInfoView>();
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
        
    }

    public bool TryChangeSword(string swordName)
    {
        bool result = _data.Sword.ToString() == swordName;

        if (result)
        {
            _switcher.Switch(_data.Sword);
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
}