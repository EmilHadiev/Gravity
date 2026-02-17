using UnityEngine;
using Zenject;

public class LocalServiceInstaller : MonoInstaller
{
    [SerializeField] private MobileCanvas _mobileCanvas;

    public override void InstallBindings()
    {
        BindOptimizator();
        BindFactory();
        BindMobileInput();
    }

    private void BindMobileInput()
    {
        Container.BindInterfacesTo<MobileCanvas>().FromComponentInNewPrefab(_mobileCanvas).AsSingle();
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