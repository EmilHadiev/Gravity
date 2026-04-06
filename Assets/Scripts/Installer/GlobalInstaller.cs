using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private EnvData _envData;
    [SerializeField] private SwordData[] _swords;
    [SerializeField] private SkinData[] _skins;
    [SerializeField] private ClothData[] _cloths;
    [SerializeField] private PlayerSoundContainer _playerSound;
    [SerializeField] private EnemySoundContainer _enemySound;
    [SerializeField] private UISoundContainer _uiSound;

    public override void InstallBindings()
    {
        BindAddresables();
        BindPlayerData();
        BindEnvData();
        BindAdv();
        BindSkinsData();
        BindSound();
        BindGameOverService();
        BindSceneLoader();
        BindCoinStorage();
        BindPauseService();
        BindSaveService();
    }

    private void BindSaveService()
    {
        Container.BindInterfacesTo<Saver>().AsSingle();
    }

    private void BindPauseService()
    {
        Container.BindInterfacesTo<PauseService>().AsSingle();
    }

    private void BindCoinStorage()
    {
        Container.BindInterfacesTo<CoinStorage>().AsSingle();
        Container.BindInterfacesTo<GemStorage>().AsSingle();
    }

    private void BindSceneLoader()
    {
        Container.BindInterfacesTo<AddressablesSceneLoader>().AsSingle();
    }

    private void BindGameOverService()
    {
        Container.BindInterfacesTo<GameOverService>().AsSingle();
    }

    private void BindSound()
    {
        Container.BindInterfacesTo<PlayerSoundContainer>().FromComponentInNewPrefab(_playerSound).AsSingle();
        Container.BindInterfacesTo<EnemySoundContainer>().FromComponentInNewPrefab(_enemySound).AsSingle();
        Container.BindInterfacesTo<UISoundContainer>().FromComponentInNewPrefab(_uiSound).AsSingle();
    }

    private void BindSkinsData()
    {
        List<SwordData> swords = new(_swords.Length);
        for (int i = 0; i < _swords.Length; i++)
        {
            var data = Instantiate(_swords[i]);
            swords.Add(data);
        }

        List<SkinData> skins = new(_skins.Length);
        for (int i = 0; i < _skins.Length; i++)
        {
            var data = Instantiate(_skins[i]);
            skins.Add(data);
        }

        List<ClothData> cloths = new(_cloths.Length);
        for (int i = 0; i < _cloths.Length; i++)
        {
            var data = Instantiate(_cloths[i]);
            cloths.Add(data);
        }

        Container.Bind<SwordData[]>().FromInstance(swords.ToArray());
        Container.Bind<SkinData[]>().FromInstance(skins.ToArray());
        Container.Bind<ClothData[]>().FromInstance(cloths.ToArray());
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

    
}