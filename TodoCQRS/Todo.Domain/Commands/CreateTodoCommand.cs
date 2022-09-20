using Flunt.Notifications;
using Flunt.Validations;
using Todo.Shared.Commands;

namespace Todo.Domain.Commands;

public class CreateTodoCommand: Notifiable<Notification>, ICommand
{
    public string Title { get; set; } = string.Empty;
    public string User { get; set; } = string.Empty;
    public DateTime Date { get; set; }

    public CreateTodoCommand()
    {
        
    }

    public CreateTodoCommand(string title, string user, DateTime date)
    {
        Title = title;
        User = user;
        Date = date;
    }
    
    public void Validate()
    {
        AddNotifications(new Contract<CreateTodoCommand>()
            .Requires()
            .IsNotNullOrEmpty(Title, "Title", "Title can't be null or empty")
            .IsNotNullOrEmpty(User, "User", "User can't be null or empty")
            .IsGreaterThan( User.Length, 6, "User", "Invalid User")
            .IsGreaterThan( Title.Length, 3, "Title", "Invalid Title")
        );
    }
}