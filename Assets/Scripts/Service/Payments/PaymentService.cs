using System;
using System.Linq;
using YG;
using Zenject;

public class PaymentService : IInitializable, IDisposable, IPaymentService
{
    private readonly ICrystallStorage _crystallStorage;
    private readonly AssetProvider.CrystallsCount[] _crystalls;

    public event Action<bool> Paid;

    public PaymentService(ICrystallStorage crystallStorage)
    {
        _crystallStorage = crystallStorage;
        _crystalls = Enum.GetValues(typeof(AssetProvider.CrystallsCount)).Cast<AssetProvider.CrystallsCount>().ToArray();
    }

    public void Initialize()
    {
        YG2.onPurchaseSuccess += SuccessPurchased;
        YG2.onPurchaseFailed += FailedPurchased;
    }

    public void Dispose()
    {
        YG2.onPurchaseSuccess -= SuccessPurchased;
        YG2.onPurchaseFailed -= FailedPurchased;
    }

    public void Purchase(string id)
    {
        YG2.BuyPayments(id);
    }

    private void SuccessPurchased(string id)
    {
        for (int i = 0; i < _crystalls.Length; i++)
        {
            if (_crystalls[i].ToString() == id)
            {
                _crystallStorage.AddMoney((int)_crystalls[i]);
                Paid?.Invoke(true);
                return;
            }
        }
    }

    private void FailedPurchased(string id)
    {
        Paid?.Invoke(false);
    }
}