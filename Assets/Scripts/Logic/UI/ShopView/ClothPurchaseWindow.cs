using Zenject;

public class ClothPurchaseWindow : PurchaseWindow, IShopWindowState
{
    [Inject] private readonly IClothSwitcherContainer _clothSwitcherContainer;
    [Inject] private readonly ICrystallStorage _crystallStorage;

    protected override void ChangeSkin()
    {        
        _clothSwitcherContainer.TrySwitchItem();
    }

    protected override bool TrySpendMoney(int price)
    {
        return _crystallStorage.TrySpendMoney(price);
    }
}