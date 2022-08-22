namespace OOP.NotificationContext
{

  public sealed class Notification
  {
    public Notification(string property, string message)
    {
      Property = property;
      Message = message;
    }

    public Notification()
    {
    }

    public string Property { get; set; } = String.Empty;
    public string Message { get; set; } = String.Empty;
    
  }
  
}