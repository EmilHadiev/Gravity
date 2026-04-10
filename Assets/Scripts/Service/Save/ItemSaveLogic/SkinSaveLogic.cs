using System.Collections.Generic;
using YG;

public class SkinSaveLogic : ItemSaveLogic
{
    public SkinSaveLogic(List<ItemSaveData> items, ItemData[] data, PlayerData playerData) : base(items, data, playerData)
    {
    }

    public override void Save()
    {
        base.Save();
        YG2.saves.Skin = PlayerData.Player;
    }

    protected override void SetItem()
    {
        PlayerData.Player = YG2.saves.Skin;
    }
}
