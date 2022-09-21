using System;
using Moq;
using Todo.Domain.Commands;
using Todo.Domain.Entities;
using Todo.Domain.Handlers;
using Todo.Domain.Repositories;
using Xunit;

namespace Todo.Tests.Handlers;

public class UpdateTodoHandlerTests
{
    private readonly ITodoRepository _todoRepository;
    private readonly TodoHandler _handler;

    private readonly Guid _todoValidId = Guid.NewGuid();
    private const string UserValid = "Superman";


    public UpdateTodoHandlerTests()
    {
        var todoItem = new Mock<TodoItem>(
                "Todo",
                DateTime.Now,
                UserValid)
            .Object;
        
        var mock = new Mock<ITodoRepository>();
        
        mock.Setup(repository => repository.GetById(_todoValidId, UserValid))
            .Returns(todoItem);

        _todoRepository = mock.Object;
        _handler = new TodoHandler(_todoRepository);
    }


    [Fact]
    public void ShouldFailWhenInvalidCommand()
    {
        var invalidCommand = new UpdateTodoCommand(
            _todoValidId,
            string.Empty,
            string.Empty);
        var result = (GenericCommandResult)_handler.Handle(invalidCommand);

        Assert.False(_handler.IsValid);
        Assert.False(result.Success);
    }

    [Fact]
    public void ShouldFailWhenInvalidTodo()
    {
        var invalidCommand = new UpdateTodoCommand(
            Guid.Empty,
            string.Empty,
            string.Empty);
        var result = (GenericCommandResult)_handler.Handle(invalidCommand);

        Assert.False(_handler.IsValid);
        Assert.False(result.Success);
    }

    [Fact]
    public void ShouldSuccessWhenValidCommand()
    {
        var validCommand = new UpdateTodoCommand(
            _todoValidId,
            "Title Todo",
            UserValid
        );
        var result = (GenericCommandResult)_handler.Handle(validCommand);

        Assert.True(_handler.IsValid);
        Assert.True(result.Success);
    }
}