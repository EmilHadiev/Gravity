using YG;

public class Saver : ISavable
{
    private readonly PlayerData _playerData;

    public Saver(PlayerData playerData)
    {
        _playerData = playerData;
    }

    public void Load()
    {
        LoadMoney();
    }

    public void Save()
    {
        SaveMoney();
    }

    #region Money
    private void LoadMoney()
    {
        _playerData.Coins = YG2.saves.Coins;
        _playerData.Gems = YG2.saves.Gems;
    }

    private void SaveMoney()
    {
        YG2.saves.Gems = _playerData.Gems;
        YG2.saves.Coins = _playerData.Coins;
    }
    #endregion
}