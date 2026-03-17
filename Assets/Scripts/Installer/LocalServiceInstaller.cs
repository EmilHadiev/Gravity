using System;
using UnityEngine;
using Zenject;

public class LocalServiceInstaller : MonoInstaller
{
    [SerializeField] private SwordSwitchContainer _container;

    public override void InstallBindings()
    {
        BindOptimizator();
        BindFactory();
        BindSwordContainer();
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