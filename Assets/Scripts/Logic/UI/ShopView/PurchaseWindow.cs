using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PurchaseWindow : MonoBehaviour, IShopWindowState
{
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private Button _purchaseButton;
    [SerializeField] private Button _rejectButton;

    [Inject] private readonly ISwordSwitchContainer _switchContainer;
    [Inject] private readonly ICoinStorage _coinStorage;
    [Inject] private readonly IUISoundContainer _soundContainer;
    [Inject] private readonly IShopWindowStateMachine _windowStateMachine;

    private ItemData _currentItemData;

    private void OnEnable()
    {
        _purchaseButton.onClick.AddListener(TryPurchase);
        _rejectButton.onClick.AddListener(RejectPurchase);
    }

    private void OnDisable()
    {
        _purchaseButton.onClick.RemoveListener(TryPurchase);
        _rejectButton.onClick.RemoveListener(RejectPurchase);
    }

    public void Enter()
    {
        gameObject.SetActive(true);
    }

    public void Exit()
    {
        gameObject.SetActive(false);
    }

    public void SetData(ItemData itemData)
    {
        _currentItemData = itemData;
    }

    private void TryPurchase()
    {
        if (_coinStorage.TrySpendCoins(_currentItemData.Price) || _currentItemData.IsPurchase)
        {
            PerformPurchase();
            _windowStateMachine.Switch<EmptyShopState>();
        }
    }

    private void PerformPurchase()
    {
        _currentItemData.IsPurchase = true;
        _soundContainer.Play(AssetProvider.Sounds.AddCoins.ToString());
        _switchContainer.TrySwitchSword();        
    }

    private void RejectPurchase()
    {
        _soundContainer.Play(AssetProvider.Sounds.Click.ToString());
        _windowStateMachine.Switch<EmptyShopState>();
    }
}