using System;
using System.Collections.Generic;

namespace YG
{
    public partial class SavesYG
    {
        public int Coins = 10000;
        public int Gems = 150;

        public List<ItemSaveData> Clothes = new List<ItemSaveData>();
    }
}

[Serializable]
public struct ItemSaveData
{
    public bool IsPurchase;
    public string ItemName;
    public bool IsEquping;
    public int Price;
}