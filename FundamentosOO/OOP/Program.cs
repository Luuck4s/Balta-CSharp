using OOP.ContentContext;

namespace OPP.Program
{

  class Program
  {

    static void Main(string[] args)
    {

      var articles = new List<Article>();
      articles.Add(new Article(
        "Artigo sobre OOP",
        "orientacao"
      ));


      var courseOOP = new Course("Fundamentos OOP", "fundamentos-oop");
      var courseCsharp = new Course("Fundamentos C#", "fundamentos-c#");



      var careers = new List<Career>();
      var careerDotnet = new Career("Fundamentos Dev", "fundamentos-dev");
      var careerItem1 = new CareerItem(1, "Comece aqui", "", null);
      var careerItem2 = new CareerItem(2, "Aprenda Dotnet", "", courseOOP);

      careerDotnet.Items.Add(careerItem2);
      careerDotnet.Items.Add(careerItem1);

      careers.Add(careerDotnet);

      foreach (var career in careers)
      {
        Console.WriteLine(career.Title);
        foreach (var item in career.Items.OrderBy(x => x.Order))
        {
          Console.WriteLine($"{item.Order} - {item.Title}");
          Console.WriteLine($"{item.Course?.Title}");


          foreach (var notification in item.Notifications)
          {
            Console.WriteLine($"{notification.Property} / {notification.Message}");
          }
        }

      }


    }
  }

}