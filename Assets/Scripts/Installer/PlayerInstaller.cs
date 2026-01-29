using Zenject;

public class PlayerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindPlayerFinder();
    }

    private void BindPlayerFinder()
    {
        Container.BindInterfacesTo<PlayerFinder>().AsSingle();
    }
}