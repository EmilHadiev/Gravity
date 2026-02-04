using UnityEngine;

[RequireComponent(typeof(TriggerObserver))]
[RequireComponent(typeof(SwitcherView))]
public class SwordSwitcher : MonoBehaviour
{
    [SerializeField] private TriggerObserver _observer;
    [SerializeField] private SwitcherView _view;
    [SerializeField] private AssetProvider.Swords _newSword;

    private void OnValidate()
    {
        _observer ??= GetComponent<TriggerObserver>();
        _view ??= GetComponent<SwitcherView>();
    }

    private void OnEnable()
    {
        _observer.Entered += OnPlayerEntered;
    }

    private void OnDisable()
    {
        _observer.Entered -= OnPlayerEntered;
    }

    private void Start()
    {
        _view.CreateSwordView(_newSword);
    }

    private void OnPlayerEntered(Collider collider)
    {
        if (collider.TryGetComponent(out ISwordSwitcher swordChanger))
            swordChanger.Switch(_newSword);
    }
}