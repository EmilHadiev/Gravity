using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class ClothSwitcher : ItemSwitcher
{
    [SerializeField] private Transform _spawnPosition;

    private IItemSetable _itemSetable;

    protected override void CreateView()
    {
        CreateCloth(Data.ItemName).Forget();
    }

    private async UniTask CreateCloth(string itemName)
    {
        var prefab = await Factory.CreateAsync(itemName);
        prefab.transform.SetPositionAndRotation(_spawnPosition.position, _spawnPosition.rotation);
        prefab.transform.parent = transform;
    }
    
    protected override void TrySetOptionToPlayerComponent(Collider collider)
    {
        base.TrySetOptionToPlayerComponent(collider);

        if (collider.TryGetComponent(out IItemSetable itemSetable))
            _itemSetable = itemSetable;
    }

    public bool TrySetItem(string itemName)
    {
        if (itemName == Data.ItemName)
        {
            _itemSetable?.SetItem(itemName);
            ChangeColor();
            return true;
        }

        return false;
    }
}