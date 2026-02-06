using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private EnvData _envData;
    [SerializeField] private SwordData[] _swords;
    [SerializeField] private PlayerSoundContainer _playerSound;

    public override void InstallBindings()
    {
        BindFactory();
        BindAddresables();
        BindPlayerData();
        BindEnvData();
        BindAdv();
        BindSwordData();
        BindSound();
    }

    private void BindSound()
    {
        Container.BindInterfacesTo<PlayerSoundContainer>().FromComponentInNewPrefab(_playerSound).AsSingle();
    }

    private void BindSwordData()
    {
        List<SwordData> swords = new List<SwordData>(_swords.Length);

        for (int i = 0; i < _swords.Length; i++)
        {
            var data = Instantiate(_swords[i]);
            swords.Add(data);
        }

        Container.Bind<SwordData[]>().FromInstance(swords.ToArray());
    }

    private void BindAdv()
    {
        Container.BindInterfacesTo<AdvService>().AsSingle();
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