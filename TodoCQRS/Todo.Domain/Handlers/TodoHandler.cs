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
    IHandler<UpdateTodoCommand>,
    IHandler<MakeTodoAsDoneCommand>,
    IHandler<MakeTodoAsUndoneCommand>
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
            AddNotification("Command", "Invalid Command");
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
        command.Validate();

        if (command.IsValid is false)
        {
            AddNotification("Command", "Invalid Command");
            return new GenericCommandResult(false, "Whops, invalid todo", command.Notifications);
        }

        var todo = _repository.GetById(command.Id, command.User);

        if (todo is null)
        {
            AddNotification("Todo", "Todo not found");
            return new GenericCommandResult(false, "Whops, invalid todo", command.Notifications);
        }

        todo.UpdateTile(command.Title);
        
        _repository.Update(todo);
        
        return new GenericCommandResult(true, "ToDo created", todo);
    }

    public ICommandResult Handle(MakeTodoAsDoneCommand command)
    {
        command.Validate();

        if (command.IsValid is false)
        {
            AddNotification("Command", "Invalid Command");
            return new GenericCommandResult(false, "Whops, invalid todo", command.Notifications);
        }

        var todo = _repository.GetById(command.Id, command.User);

        if (todo is null)
        {
            AddNotification("Todo", "Todo not found");
            return new GenericCommandResult(false, "Whops, invalid todo", command.Notifications);
        }

        todo.MarkAsDone();
        
        _repository.Update(todo);
        
        return new GenericCommandResult(true, "ToDo created", todo);
    }

    public ICommandResult Handle(MakeTodoAsUndoneCommand command)
    {
        command.Validate();

        if (command.IsValid is false)
        {
            AddNotification("Command", "Invalid Command");
            return new GenericCommandResult(false, "Whops, invalid todo", command.Notifications);
        }

        var todo = _repository.GetById(command.Id, command.User);

        if (todo is null)
        {
            AddNotification("Todo", "Todo not found");
            return new GenericCommandResult(false, "Whops, invalid todo", command.Notifications);
        }

        todo.MarkAsUndone();
        
        _repository.Update(todo);
        
        return new GenericCommandResult(true, "ToDo created", todo);
    }
}