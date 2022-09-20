using Flunt.Notifications;
using Flunt.Validations;
using Todo.Shared.Commands;

namespace Todo.Domain.Commands;

public class MakeTodoAsDoneCommand: Notifiable<Notification>, ICommand
{
    public Guid Id { get; set; }
    public string User { get; set; }

    public MakeTodoAsDoneCommand()
    {

    }
    
    public MakeTodoAsDoneCommand(Guid id, string user)
    {
        Id = id;
        User = user;
    }

    public void Validate()
    {
        AddNotifications(
            new Contract<MakeTodoAsDoneCommand>()
                .Requires()
                .IsGreaterThan(User.Length, 6, "User", "Invalid User"));
    }
}