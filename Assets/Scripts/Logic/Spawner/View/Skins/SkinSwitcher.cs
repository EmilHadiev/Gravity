using Cysharp.Threading.Tasks;
using System;

public class SkinSwitcher : ItemSwitcher
{
    protected override void CreateView()
    {
        var skinCreator = new SkinViewCreator(Factory, transform);
        skinCreator.CreateSkinView(Data.ItemName + AssetProvider.PlayerViewPrefix).Forget();
    }

    public bool TrySetSkin(string skinName, Action ChangeSkin)
    {
        if (skinName == Data.ItemName)
        {
            ChangeSkin?.Invoke();
            return true;
        }

        return false;
    }
}