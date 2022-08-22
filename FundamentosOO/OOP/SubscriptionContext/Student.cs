using OOP.SharedContext;

namespace OOP.SubscriptionContext
{
  public class Student : Base
  {

    public Student(User user, string name, string email)
    {
      User = user;
      Name = name;
      Email = email;
      Subscriptions = new List<Subscription>();
    }

    public User User { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public IList<Subscription> Subscriptions { get; set; }

    public void CreateSubscription(Subscription subscription)
    {

      if (IsPremium)
      {
        AddNotification(new NotificationContext.Notification(
         "Premium",
         "Student already has a active subscription"
        ));
        return;
      }

       Subscriptions.Add(subscription);
    }

    public bool IsPremium => Subscriptions.Any(x => !x.isInactive);
  }

}