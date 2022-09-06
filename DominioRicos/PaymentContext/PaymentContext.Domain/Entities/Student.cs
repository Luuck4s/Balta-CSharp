using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities;

public class Student: Entity
{
    private IList<Subscription> _subscriptions;
    public Name Name { get; set; }
    public Document Document { get; private set; }
    public Email Email { get; private set; }
    public Address Address { get; private set; }
    public IReadOnlyCollection<Subscription> Subscriptions => _subscriptions.ToArray();

    public Student(
        Name name,
        Document document,
        Email email,
        Address address
    )
    {
        Name = name;
        Document = document;
        Email = email;
        Address = address;
        _subscriptions = new List<Subscription>();
        
        AddNotifications(name, document, email);
    }

    public void AddSubscription(Subscription subscription)
    {
        foreach (var sub in _subscriptions)
        {
            sub.Inactivate();
        }

        _subscriptions.Add(subscription);
    }
}