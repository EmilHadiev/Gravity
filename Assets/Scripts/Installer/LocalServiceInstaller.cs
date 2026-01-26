using Zenject;

public class LocalServiceInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindOptimizator();
    }

    private void BindOptimizator()
    {
        Container.BindInterfacesTo<TweenOptimizator>().AsSingle().NonLazy();
    }
}