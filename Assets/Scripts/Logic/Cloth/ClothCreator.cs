using Cysharp.Threading.Tasks;
using System;
using System.Linq;
using UnityEngine;
using Zenject;

public class ClothCreator : MonoBehaviour, IItemSetable
{
    [SerializeField] private ClothPlace[] _itemPlaces;

    [Inject] private readonly IFactory _factory;

    [Inject] private readonly ClothData[] _clothes;

    private void OnValidate()
    {
        if (_itemPlaces.Length == 0)
            _itemPlaces = GetComponentsInChildren<ClothPlace>();
    }

    private void Start()
    {
        for (int i = 0; i < _clothes.Length; i++)
        {
            if (_clothes[i].IsEquping)
                SetItem(_clothes[i].ItemName);
        }
    }

    public void SetItem(string itemName)
    {
        CreateItemAsync(itemName).Forget();
    }

    private async UniTask CreateItemAsync(string item)
    {
        var prefab = await _factory.CreateAsync(item.ToString());
        var itemPlace = GetItemPlace(item);

        prefab.transform.parent = itemPlace.transform;

        var itemPrefab = prefab.GetComponent<Item>();
        itemPrefab.SetScale(itemPlace.Scale);
        itemPrefab.SetPositionAndRotation(itemPlace.Position, itemPlace.Rotation);
    }

    private ClothPlace GetItemPlace(string itemName)
    {
        var itemPalce = _itemPlaces.FirstOrDefault(i => i.Item.ToString() == itemName);
        return itemPalce != null ? itemPalce : throw new ArgumentException(nameof(itemName));
    }
}