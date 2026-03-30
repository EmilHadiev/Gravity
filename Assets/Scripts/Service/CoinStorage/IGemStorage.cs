using System;

public interface IGemStorage
{
    event Action<int> GemsChanged;
    int Gems { get; }

    void AddCoins(int gems);
    bool TrySpendCoins(int gems);
}