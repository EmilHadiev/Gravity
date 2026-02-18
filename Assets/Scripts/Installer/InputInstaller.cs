using UnityEngine;
using Zenject;

public class InputInstaller : MonoInstaller
{
    [SerializeField] private MobileCanvas _mobileCanvas;

    private EnvData _envData;

    public override void InstallBindings()
    {
        BindInput();
    }

    private void BindInput()
    {
        if (_envData.IsDesktop)
            Container.BindInterfacesTo<DesktopInput>().AsSingle();
        else
            Container.BindInterfacesTo<MobileCanvas>().FromComponentInNewPrefab(_mobileCanvas).AsSingle();
    }

    [Inject]
    private void Constructor(EnvData envData, IFactory factory)
    {
        _envData = envData;
    }
}