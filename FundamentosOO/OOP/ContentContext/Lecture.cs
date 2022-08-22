using OOP.ContentContext.Enums;
using OOP.SharedContext;

namespace OOP.ContentContext
{
  public class Lecture: Base
  {
    public int Order { get; set; }
    public string Title { get; set; } = String.Empty;
    public int DurationInMinutes { get; set; }

    public EContentLevel Level { get; set; }
  }
}