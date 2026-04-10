using System;
using System.Collections.Generic;
using System.Linq;

public abstract class ItemSaveLogic : IItemSaveLogic, IDisposable
{
    private readonly List<ItemSaveData> _items;
    private readonly ItemData[] _data;
    private readonly Dictionary<string, ItemSaveData> _savedItems;

    protected readonly PlayerData PlayerData;

    public ItemSaveLogic(List<ItemSaveData> items, ItemData[] data, PlayerData playerData)
    {
        _items = items;
        _data = data;
        PlayerData = playerData;

        _savedItems = items.ToDictionary(i => i.ItemName, i => i);
    }

    public void Load()
    {
        Init();
    }

    public virtual void Save()
    {
        for (int i = 0; i < _data.Length; i++)
        {
            ItemSaveData item = CreateSaveData(i);
            _items[i] = item;
        }
    }

    public void Dispose()
    {
        Save();
    }

    protected abstract void SetItem();

    private void Init()
    {
        if (_items.Count == 0)
        {
            FirstInit();
        }
        else
        {
            DefaultInit();
        }
    }

    private void FirstInit()
    {
        _items.Clear();

        for (int i = 0; i < _data.Length; i++)
        {
            ItemSaveData item = CreateSaveData(i);
            _items.Add(item);
        }
    }

    private void DefaultInit()
    {
        for (int i = 0; i < _data.Length; i++)
        {
            if (_savedItems.TryGetValue(_data[i].ItemName, out ItemSaveData savedItem))
            {
                _data[i].IsPurchase = savedItem.IsPurchase;
                SetItem();
            }
        }
    }

    private ItemSaveData CreateSaveData(int index)
    {
        return new ItemSaveData()
        {
            IsPurchase = _data[index].IsPurchase,
            ItemName = _data[index].ItemName,
        };
    }
}