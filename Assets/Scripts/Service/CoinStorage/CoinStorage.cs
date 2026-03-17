using System;

public class CoinStorage : ICoinStorage
{
    public int Coins { get; private set; } = 10050;

    public event Action<int> CoinsChanged;

    public void AddCoins(int coins)
    {
        if (coins == 0)
            return;

        Coins += coins;
        CoinsChanged?.Invoke(coins);
    }

    public bool TrySpendCoins(int coins)
    {
        if (Coins - coins < 0 || coins < 0)
            return false;

        Coins -= coins;
        CoinsChanged?.Invoke(Coins);
        return true;
    }
}