using Zenject;

public class SkinPurchaseWindow : PurchaseWindow, IShopWindowState
{
    [Inject] private readonly ISkinSwitcherContainer _skinContainer;
    [Inject] private readonly ISceneLoader _sceneLoader;

    protected override void ChangeSkin()
    {
        _skinContainer.TrySwitchSkin();
        _sceneLoader.Restart();
    }
}