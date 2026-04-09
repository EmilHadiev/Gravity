using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public abstract class PurchaseWindow : MonoBehaviour
{
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private Button _purchaseButton;
    [SerializeField] private Button _rejectButton;

    [Inject] private readonly ICoinStorage _coinStorage;
    [Inject] private readonly IUISoundContainer _soundContainer;
    [Inject] private readonly IShopWindowStateMachine _windowStateMachine;
    [Inject] private readonly ISavable _saver;

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
        if (_currentItemData.IsPurchase)
        {
            PerformPurchase();
        }
        else
        {
            gameObject.SetActive(true);
        }  
    }

    public void Exit()
    {
        gameObject.SetActive(false);
    }

    public void SetData(ItemData itemData)
    {
        _currentItemData = itemData;
        _priceText.text = $"{_currentItemData.Price}";
    }

    private void TryPurchase()
    {
        if (_coinStorage.TrySpendMoney(_currentItemData.Price))
            PerformPurchase();
    }

    private void PerformPurchase()
    {
        ChangeSkin();
        TrySave();        
        _soundContainer.Play(AssetProvider.Sounds.AddCoins.ToString());
        _windowStateMachine.Switch<EmptyShopState>();
    }

    private void TrySave()
    {
        if (_currentItemData.IsPurchase == false)
        {
            _currentItemData.IsPurchase = true;
            AdditionalSaveLogic();
            _saver.Save();
        }
    }

    protected virtual void AdditionalSaveLogic()
    {

    }

    private void RejectPurchase()
    {
        _soundContainer.Play(AssetProvider.Sounds.Click.ToString());
        _windowStateMachine.Switch<EmptyShopState>();
    }

    protected abstract void ChangeSkin();
}