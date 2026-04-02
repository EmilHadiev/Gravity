using Cysharp.Threading.Tasks;
using System;
using System.Linq;
using UnityEngine;
using Zenject;

public class ItemCreator : MonoBehaviour
{
    [SerializeField] private ItemPlace[] _itemPlaces;

    [Inject] private readonly IFactory _factory;

    private void OnValidate()
    {
        if (_itemPlaces.Length == 0)
            _itemPlaces = GetComponentsInChildren<ItemPlace>();
    }

    [ContextMenu(nameof(Start))]
    private void Start()
    {
        for (int i = 0; i < _itemPlaces.Length; i++)
        {
            CreateItem(_itemPlaces[i].Item);
        }
    }

    public void CreateItem(AssetProvider.Item item)
    { 
        CreateItemAsync(item).Forget();
    }

    private async UniTask CreateItemAsync(AssetProvider.Item item)
    {
        var prefab = await _factory.CreateAsync(item.ToString());
        var itemPlace = GetItemPlace(item);

        prefab.transform.parent = itemPlace.transform;
       
        var itemPrefab = prefab.GetComponent<Item>();
        itemPrefab.SetScale(itemPlace.Scale);
        itemPrefab.SetPositionAndRotation(itemPlace.Position, itemPlace.Rotation);
    }

    private ItemPlace GetItemPlace(AssetProvider.Item item)
    {
        var itemPalce = _itemPlaces.FirstOrDefault(i => i.Item == item);
        return itemPalce != null ? itemPalce : throw new ArgumentException(nameof(item));
    }
}