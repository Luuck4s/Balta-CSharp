using Flunt.Notifications;
using Todo.Domain.Commands;
using Todo.Domain.Entities;
using Todo.Domain.Repositories;
using Todo.Shared.Commands;
using Todo.Shared.Handlers;

namespace Todo.Domain.Handlers;

public class TodoHandler :
    Notifiable<Notification>,
    IHandler<CreateTodoCommand>,
    IHandler<UpdateTodoCommand>
{
    private readonly ITodoRepository _repository;

    public TodoHandler(ITodoRepository repository)
    {
        _repository = repository;
    }

    public ICommandResult Handle(CreateTodoCommand command)
    {
        command.Validate();

        if (command.IsValid is false)
        {
            return new GenericCommandResult(false, "Whops, invalid todo", command.Notifications);
        }

        var todo = new TodoItem(
            command.Title,
            command.Date,
            command.User);

        _repository.Create(todo);
        
        return new GenericCommandResult(true, "ToDo created", todo);
    }

    public ICommandResult Handle(UpdateTodoCommand command)
    {
        throw new NotImplementedException();
    }
}