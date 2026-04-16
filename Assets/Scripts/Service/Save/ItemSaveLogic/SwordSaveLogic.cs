using System.Collections.Generic;
using YG;

public class SwordSaveLogic : ItemSaveLogic
{
    public SwordSaveLogic(List<ItemSaveData> items, ItemData[] data, PlayerData playerData) : base(items, data, playerData)
    {

    }

    public override void Save()
    {
        base.Save();
        YG2.saves.CurrentSword = PlayerData.Swords;
    }

    protected override void SetItem()
    {
        PlayerData.Swords = YG2.saves.CurrentSword;
    }
}