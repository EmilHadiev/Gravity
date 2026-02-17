using Cysharp.Threading.Tasks;
using System;

public class MobileInputCreator : IMobileInputCreator
{
    public event Action<IMobileInput> InputCreated;
    public IMobileInput MobileInput { get; private set; }

    public MobileInputCreator(IFactory factory, EnvData envData)
    {
        if (envData.IsDesktop == false)
            CreateMobileCanvas(factory).Forget();
    }

    private async UniTask CreateMobileCanvas(IFactory factory)
    {
        var prefab = await factory.CreateAsync(AssetProvider.MobileCanvas);
        var input = prefab.GetComponent<IMobileInput>();
        MobileInput = input;
    }
}