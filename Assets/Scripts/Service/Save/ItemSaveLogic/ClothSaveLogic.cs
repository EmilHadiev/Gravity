using System.Collections.Generic;
using System.Linq;

public class ClothSaveLogic : IItemSaveLogic
{
    private readonly List<ClothSaveData> _items;
    private readonly ClothData[] _clothData;

    private readonly Dictionary<string, ClothData> _clothConfigDict;

    public ClothSaveLogic(List<ClothSaveData> items, ClothData[] clothData)
    {
        _items = items;
        _clothData = clothData;

        _clothConfigDict = clothData.ToDictionary(c => c.ItemName, c => c);
    }

    public void Save()
    {
        for (int i = 0; i < _items.Count; i++)
        {
            if (_clothConfigDict.TryGetValue(_items[i].ItemName, out var clothData))
            {
                _items[i] = CreateSaveData(clothData);
            }
        }
    }

    public void Load()
    {
        if (_items.Count == 0)
            FirstInit();
        else
            DefaultInit();
    }

    private void FirstInit()
    {
        _items.Clear();
        foreach (var data in _clothData)
        {
            _items.Add(CreateSaveData(data));
        }
    }

    private void DefaultInit()
    {
        var itemSaveDict = _items.ToDictionary(it => it.ItemName, it => it);

        foreach (var cloth in _clothData)
        {
            if (itemSaveDict.TryGetValue(cloth.ItemName, out ClothSaveData savedItem))
            {
                cloth.IsPurchase = savedItem.IsPurchase;
                cloth.IsEquping = savedItem.IsEquping;
            }
        }
    }

    private ClothSaveData CreateSaveData(ClothData data)
    {
        return new ClothSaveData
        {
            ItemName = data.ItemName,
            IsPurchase = data.IsPurchase,
            IsEquping = data.IsEquping
        };
    }
}