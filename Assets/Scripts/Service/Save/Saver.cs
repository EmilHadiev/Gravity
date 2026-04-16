using System;
using YG;

public class Saver : ISavable, IDisposable
{
    private readonly PlayerData _playerData;
    private readonly ICoinStorage _coinStorage;
    private readonly ICrystallStorage _crystallStorage;

    private readonly IItemSaveLogic _clothSave;
    private readonly IItemSaveLogic _swordsSave;
    private readonly IItemSaveLogic _skinSave;

    public Saver(PlayerData playerData, ICoinStorage coinStorage, ICrystallStorage gemStorage, 
        ClothData[] clothes, SkinData[] skins, SwordData[] swords)
    {
        _playerData = playerData;
        _coinStorage = coinStorage;
        _crystallStorage = gemStorage;

        SavesYG saves = YG2.saves;
        _clothSave = new ClothSaveLogic(saves.Clothes, clothes);
        _swordsSave = new SwordSaveLogic(saves.Swords, swords, _playerData);
        _skinSave = new SkinSaveLogic(saves.Skins, skins, _playerData);
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
        _coinStorage.AddMoney(YG2.saves.Coins);
        _crystallStorage.AddMoney(YG2.saves.Crystalls);
    }

    private void SaveMoney()
    {
        YG2.saves.Crystalls = _crystallStorage.Money;
        YG2.saves.Coins = _coinStorage.Money;
    }
    #endregion

    #region ItemSaveLogic
    private void SaveItems()
    {
        _clothSave.Save();
        _swordsSave.Save();
        _skinSave.Save();
    }

    private void LoadItems()
    {
        _clothSave.Load();
        _swordsSave.Load();
        _skinSave.Load();
    }
    #endregion
}