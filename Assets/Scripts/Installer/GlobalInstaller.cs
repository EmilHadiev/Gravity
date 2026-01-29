using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private EnvData _envData;

    public override void InstallBindings()
    {
        BindFactory();
        BindAddresables();
        BindPlayerData();
        BindEnvData();
    }

    private void BindEnvData()
    {
        Container.Bind<EnvData>().FromNewScriptableObject(_envData).AsSingle();
    }

    private void BindPlayerData()
    {
        Container.Bind<PlayerData>().FromNewScriptableObject(_playerData).AsSingle();
    }

    private void BindAddresables()
    {
        Container.BindInterfacesTo<AddressablesLoader>().AsSingle();
    }

    private void BindFactory()
    {
        Container.BindInterfacesTo<Factory>().AsSingle();
        Container.BindInterfacesTo<ParticleFactory>().AsSingle();
    }
}