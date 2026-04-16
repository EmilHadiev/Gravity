using System;

public interface IPaymentService
{
    event Action<bool> Paid;
    void Purchase(string id);
}