using System;

public class CoinStorage : ICoinStorage
{
    public int Money { get; private set; }

    public event Action<int> MoneyChanged;

    public void AddMoney(int coins)
    {
        if (coins == 0)
            return;

        Money += coins;
        MoneyChanged?.Invoke(coins);
    }

    public bool TrySpendMoney(int coins)
    {
        if (Money - coins < 0 || coins < 0)
            return false;

        Money -= coins;
        MoneyChanged?.Invoke(Money);
        return true;
    }
}