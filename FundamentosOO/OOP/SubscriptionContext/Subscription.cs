using OOP.SharedContext;

namespace OOP.SubscriptionContext
{

  public class Subscription: Base
  {
    public Subscription(Plan plan, DateTime? endDate)
    {
      Plan = plan;
      EndDate = endDate;
    }

    public Plan Plan { get; set; }
    public DateTime? EndDate { get; set; }

    public bool isInactive => EndDate <= DateTime.Now;
  }
  
}