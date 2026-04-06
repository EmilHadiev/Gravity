using System;

public class GemStorage : IGemStorage
{
    public int Money { get; private set; }

    public event Action<int> MoneyChanged;

    public void AddMoney(int gems)
    {
        if (gems == 0)
            return;

        Money += gems;
        MoneyChanged?.Invoke(gems);
    }

    public bool TrySpendMoney(int gems)
    {
        if (Money - gems < 0 || gems < 0)
            return false;

        Money -= gems;
        MoneyChanged?.Invoke(Money);
        return true;
    }
}