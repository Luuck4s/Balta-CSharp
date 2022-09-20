using Flunt.Notifications;
using Flunt.Validations;
using Todo.Shared.Commands;

namespace Todo.Domain.Commands;

public class MakeTodoAsUndoneCommand: Notifiable<Notification>, ICommand
{
    public Guid Id { get; set; }
    public string User { get; set; }

    public MakeTodoAsUndoneCommand()
    {

    }
    
    public MakeTodoAsUndoneCommand(Guid id, string user)
    {
        Id = id;
        User = user;
    }

    public void Validate()
    {
        AddNotifications(
            new Contract<MakeTodoAsUndoneCommand>()
                .Requires()
                .IsGreaterThan(User.Length, 6, "User", "Invalid User"));
    }
}