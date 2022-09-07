using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities;

public class Student : Entity
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
        Email email
    )
    {
        Name = name;
        Document = document;
        Email = email;
        _subscriptions = new List<Subscription>();

        AddNotifications(name, document, email);
    }

    public void AddSubscription(Subscription subscription)
    {
        
        var hasSubscriptionActive = false;

        foreach (var sub in _subscriptions)
        {
            if (sub.Active)
            {
                hasSubscriptionActive = true;
            }
        }

        AddNotifications(
            new Contract<Student>()
                .Requires()
                .IsFalse(
                    hasSubscriptionActive,
                    "Student.Subscriptions",
                    "Student already has subscription active")
                .AreNotEquals(
                    0, 
                    subscription.Payments.Count, 
                    "Student.Subscription.Payments", 
                    "Subscription had no payment")
        );

        if (IsValid)
        {
            _subscriptions.Add(subscription);
        }
    }
}