using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

public interface IAddressablesLoader
{
    UniTask<T> LoadAssetAsync<T>(string assetPath) where T : UnityEngine.Object;

    UniTask<T> LoadAssetAsync<T>(AssetReference reference) where T : UnityEngine.Object;

    void Release(string assetPath);
    void Release(AssetReference reference);
    void ReleaseAll();
}