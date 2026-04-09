using Zenject;

public class ClothPurchaseWindow : PurchaseWindow, IShopWindowState
{
    [Inject] private readonly IClothSwitcherContainer _clothSwitcherContainer;

    protected override void ChangeSkin()
    {        
        _clothSwitcherContainer.TrySwitchItem();
    }
}