﻿namespace PaymentContext.Domain.Entities;

public class Subscription
{
    private IList<Payment> _payments;
    public DateTime CreateDate { get; private set; }
    public DateTime LastUpdateDate { get; private set; }
    public DateTime? ExpireDate { get; private set; }
    public bool Active { get; private set; }
    
    public Subscription(DateTime? expireDate)
    {
        ExpireDate = expireDate;
        CreateDate = DateTime.Now;
        Active = true;
        _payments = new List<Payment>();
    }
    
    public IReadOnlyCollection<Payment> Payments => _payments.ToArray();

    public void AddPayment(Payment payment)
    {
        _payments.Add(payment);
    }
    
    public void Activate()
    {
        Active = true;
        LastUpdateDate = DateTime.Now;
    }

    public void Inactivate()
    {
        Active = false;
        LastUpdateDate = DateTime.Now;
    }
}