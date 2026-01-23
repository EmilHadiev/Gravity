using Zenject;
using UnityEngine;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField] private PlayerData _playerData;

    public override void InstallBindings()
    {
        BindFactory();
        BindAddresables();
        BindPlayerData();
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
    }
}