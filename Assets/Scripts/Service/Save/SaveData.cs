using System;
using System.Collections.Generic;

namespace YG
{
    public partial class SavesYG
    {
        public int Coins = 10000;
        public int Crystalls = 150;

        public List<ClothSaveData> Clothes = new List<ClothSaveData>();
        public List<ItemSaveData> Swords = new List<ItemSaveData>();
        public List<ItemSaveData> Skins = new List<ItemSaveData>();

        public AssetProvider.Swords CurrentSword;
        public AssetProvider.Player CurrentSkin;
    }
}

[Serializable]
public struct ItemSaveData
{
    public bool IsPurchase;
    public string ItemName;    
}

[Serializable]
public struct ClothSaveData
{
    public bool IsPurchase;
    public string ItemName;
    public bool IsEquping;
}