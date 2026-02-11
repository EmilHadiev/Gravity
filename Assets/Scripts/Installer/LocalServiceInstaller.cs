using Zenject;

public class LocalServiceInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindOptimizator();
        BindFactory();
    }

    private void BindOptimizator()
    {
        Container.BindInterfacesTo<TweenOptimizator>().AsSingle().NonLazy();
    }

    private void BindFactory()
    {
        Container.BindInterfacesTo<Factory>().AsSingle();
        Container.BindInterfacesTo<ParticleFactory>().AsSingle();
    }
}