using Todo.Shared.Commands;

namespace Todo.Shared.Handlers;

public interface IHandler<T>
    where T : ICommand
{
    ICommandResult Handle(T command);
}