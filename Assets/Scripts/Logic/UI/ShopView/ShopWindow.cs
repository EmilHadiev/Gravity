using UnityEngine;
using Zenject;

public class ShopWindow : MonoBehaviour
{
    [SerializeField] private PurchaseWindow _purchaseWindow;

    [Inject] private readonly ISwordSwitchContainer _swordSwitcher;

    private void Start()
    {
        _swordSwitcher.PlayerEntered += OnPlayerEntered;
        _swordSwitcher.PlayerExited += OnPlayerExited;

        _purchaseWindow.Closing += CloseWindow;

        EnableToggle(false);
    }

    private void OnDestroy()
    {
        _swordSwitcher.PlayerEntered -= OnPlayerEntered;
        _swordSwitcher.PlayerExited -= OnPlayerExited;

        _purchaseWindow.Closing -= CloseWindow;
    }

    private void OnPlayerEntered(ItemData data)
    {
        EnableToggle(true);
        _purchaseWindow.SetData(data);
    }

    private void OnPlayerExited()
    {
        EnableToggle(false);
    }

    private void EnableToggle(bool isOn)
    {
        gameObject.SetActive(isOn);
    }

    private void CloseWindow()
    {
        EnableToggle(false);
    }
}
