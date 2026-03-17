using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PurchaseWindow : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private Button _purchaseButton;
    [SerializeField] private Button _rejectButton;

    [Inject] private readonly ISwordSwitchContainer _switchContainer;
    [Inject] private readonly ICoinStorage _coinStorage;

    private ItemData _currentItemData;

    private void OnValidate()
    {
        _canvas ??= GetComponentInParent<Canvas>();
    }

    private void OnEnable()
    {
        _switchContainer.PlayerEntered += OnPlyerEntered;
        _switchContainer.PlayerExited += OnPlayerExited;

        _purchaseButton.onClick.AddListener(TryPurchase);
        _rejectButton.onClick.AddListener(RejectPurchase);
    }

    private void OnDisable()
    {
        _switchContainer.PlayerEntered -= OnPlyerEntered;
        _switchContainer.PlayerExited -= OnPlayerExited;

        _purchaseButton.onClick.RemoveListener(TryPurchase);
        _rejectButton.onClick.RemoveListener(RejectPurchase);
    }

    private void OnPlayerExited()
    {
        CanvasEnableToggle(false);
    }

    private void OnPlyerEntered(ItemData data)
    {
        _currentItemData = data;
        CanvasEnableToggle(true);
    }

    private void TryPurchase()
    {
        if (_coinStorage.TrySpendCoins(_currentItemData.Price))
        {
            _switchContainer.TrySwitchSword();
            CanvasEnableToggle(false);
        }
    }

    private void RejectPurchase()
    {
        _canvas.enabled = false;
    }

    private void CanvasEnableToggle(bool isOn) => _canvas.enabled = isOn;
}