using UnityEngine;
using Zenject;

public class LocalServiceInstaller : MonoInstaller
{
    [SerializeField] private SwordSwitchContainer _container;
    [SerializeField] private ShopWindow _shopWindow;

    public override void InstallBindings()
    {
        BindOptimizator();
        BindFactory();
        BindShopWindow();
        BindSwordContainer();
    }

    private void BindShopWindow()
    {
        Container.BindInterfacesTo<ShopWindow>().FromInstance(_shopWindow).AsSingle();
    }

    private void BindSwordContainer()
    {
        Container.BindInterfacesTo<SwordSwitchContainer>().FromComponentInNewPrefab(_container).AsSingle();
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