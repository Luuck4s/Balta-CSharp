using Store.Shared.Commands;

namespace Store.Shared.Handlers;

public interface IHandler<T>
    where T : ICommand
{
    ICommandResult Handle(T command);
}