using Cysharp.Threading.Tasks;
using UnityEngine;

public class SkinViewCreator
{
    private readonly IFactory _factory;
    private readonly Transform _spawner;

    public SkinViewCreator(IFactory factory, Transform position)
    {
        _factory = factory;
        _spawner = position;
    }

    public async UniTask CreateSkinView(string itemName)
    {
        var prefab = await _factory.CreateAsync(itemName);
        prefab.transform.SetPositionAndRotation(_spawner.position, _spawner.rotation);
        prefab.transform.parent = _spawner.transform;
    }
}