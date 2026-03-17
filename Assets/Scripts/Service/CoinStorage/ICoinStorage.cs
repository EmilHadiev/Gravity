using System;

public interface ICoinStorage
{
    event Action<int> CoinsChanged;
    int Coins { get; }

    void AddCoins(int coins);
    bool TrySpendCoins(int coins);
}