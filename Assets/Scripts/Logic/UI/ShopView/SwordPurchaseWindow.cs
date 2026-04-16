using Zenject;

public class SwordPurchaseWindow : PurchaseWindow, IShopWindowState
{
    [Inject] private readonly ISwordSwitchContainer _switchContainer;
    [Inject] private readonly ICoinStorage _cointStorage;

    protected override void ChangeSkin()
    {
        _switchContainer.TrySwitchSword();
    }

    protected override bool TrySpendMoney(int price)
    {
        return _cointStorage.TrySpendMoney(price);
    }
}