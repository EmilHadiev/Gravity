using Zenject;

public class SwordPurchaseWindow : PurchaseWindow, IShopWindowState
{
    [Inject] private readonly ISwordSwitchContainer _switchContainer;

    protected override void ChangeSkin()
    {
        _switchContainer.TrySwitchSword();
    }
}