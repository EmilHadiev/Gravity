using Zenject;

public class GlobalInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindFactory();
        BindAddresables();
    }

    private void BindAddresables()
    {
        Container.BindInterfacesTo<AddressablesLoader>().AsSingle();
    }

    private void BindFactory()
    {
        Container.BindInterfacesTo<Factory>().AsSingle();
    }
}