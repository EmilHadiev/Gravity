using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class ClothSwitcher : ItemSwitcher
{
    [SerializeField] private Transform _spawnPosition;

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

    public bool TrySetItem(string itemName, Action ChangeItem)
    {
        if (itemName == Data.ItemName)
        {
            ChangeItem?.Invoke();
            return true;
        }

        return false;
    }
}