using UnityEngine;
using Zenject;

public class LocalServiceInstaller : MonoInstaller
{
    [SerializeField] private SwordSwitchContainer _swordContainer;
    [SerializeField] private SkinSwitcherContainer _skinContainer;
    [SerializeField] private ClothSwitcherContainer _clothSwitcherContainer;
    [SerializeField] private ShopWindow _shopWindow;

    public override void InstallBindings()
    {
        BindOptimizator();
        BindFactory();
        BindShopWindow();
        BindContainers();
    }

    private void BindShopWindow()
    {
        Container.BindInterfacesTo<ShopWindow>().FromInstance(_shopWindow).AsSingle();
    }

    private void BindContainers()
    {
        Container.BindInterfacesTo<SwordSwitchContainer>().FromInstance(_swordContainer).AsSingle();
        Container.BindInterfacesTo<SkinSwitcherContainer>().FromInstance(_skinContainer).AsSingle();
        Container.BindInterfacesTo<ClothSwitcherContainer>().FromInstance(_clothSwitcherContainer).AsSingle();
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