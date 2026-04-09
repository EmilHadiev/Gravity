using System;
using YG;

public class Saver : ISavable, IDisposable
{
    private readonly PlayerData _playerData;
    private readonly ICoinStorage _coinStorage;
    private readonly IGemStorage _gemStorage;

    private readonly IItemSaveLogic _clothSave;

    public Saver(PlayerData playerData, ICoinStorage coinStorage, IGemStorage gemStorage, ClothData[] _clothes)
    {
        _playerData = playerData;
        _coinStorage = coinStorage;
        _gemStorage = gemStorage;

        var saves = YG2.saves;
        _clothSave = new ClothSaveLogic(saves.Clothes, _clothes);
    }

    public void Load()
    {
        LoadMoney();
        LoadItems();
    }

    public void Save()
    {
        SaveMoney();
        SaveItems();

        YG2.SaveProgress();
    }

    public void Dispose()
    {
        Save();
    }

    public void ResetAllProgress()
    {
        YG2.SetDefaultSaves();
    }

    #region Money
    private void LoadMoney()
    {
        _playerData.Coins = YG2.saves.Coins;
        _playerData.Gems = YG2.saves.Gems;

        _coinStorage.AddMoney(_playerData.Coins);
        _gemStorage.AddMoney(_playerData.Gems);
    }

    private void SaveMoney()
    {
        YG2.saves.Gems = _playerData.Gems;
        YG2.saves.Coins = _playerData.Coins;
    }
    #endregion

    #region ItemSaveLogic
    private void SaveItems()
    {
        _clothSave.Save();
    }

    private void LoadItems()
    {
        _clothSave.Load();
    }
    #endregion
}