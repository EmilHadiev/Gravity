using System;

public interface IMoneyStorage
{
    event Action<int> MoneyChanged;
    int Money { get; }

    void AddMoney(int value);
    bool TrySpendMoney(int value);
}