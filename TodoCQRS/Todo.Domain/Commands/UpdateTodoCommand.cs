using Flunt.Notifications;
using Flunt.Validations;
using Todo.Shared.Commands;

namespace Todo.Domain.Commands;

public class UpdateTodoCommand: Notifiable<Notification>, ICommand
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string User { get; set; }

    public UpdateTodoCommand()
    {
    }

    public UpdateTodoCommand(Guid id, string title, string user)
    {
        Id = id;
        Title = title;
        User = user;
    }

    public void Validate()
    {
        AddNotifications(new Contract<UpdateTodoCommand>()
            .Requires()
            .IsNotNullOrEmpty(Title, "Title", "Title can't be null or empty")
            .IsNotNullOrEmpty(User, "User", "User can't be null or empty")
            .IsGreaterThan(User.Length, 6, "User", "Invalid User")
            .IsGreaterThan(Title.Length, 3, "Title", "Invalid Title"));
    }
}