using System;

public class GemStorage : IGemStorage
{
    public int Gems { get; private set; } = 500;

    public event Action<int> GemsChanged;

    public void AddCoins(int gems)
    {
        if (gems == 0)
            return;

        Gems += gems;
        GemsChanged?.Invoke(gems);
    }

    public bool TrySpendCoins(int gems)
    {
        if (Gems - gems < 0 || gems < 0)
            return false;

        Gems -= gems;
        GemsChanged?.Invoke(Gems);
        return true;
    }
}