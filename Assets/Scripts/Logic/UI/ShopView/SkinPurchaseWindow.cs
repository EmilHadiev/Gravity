using Zenject;

public class SkinPurchaseWindow : PurchaseWindow, IShopWindowState
{
    [Inject] private readonly ISkinSwitcherContainer _skinContainer;
    [Inject] private readonly ISceneLoader _sceneLoader;
    [Inject] private readonly ICrystallStorage _gemStorage;

    protected override void ChangeSkin()
    {
        _skinContainer.TrySwitchSkin();
        _sceneLoader.Restart();
    }

    protected override bool TrySpendMoney(int price)
    {
        return _gemStorage.TrySpendMoney(price);
    }
}