using System;
using Moq;
using Todo.Domain.Commands;
using Todo.Domain.Handlers;
using Todo.Domain.Repositories;
using Xunit;

namespace Todo.Tests.Handlers;

public class CreateTodoHandlerTests
{
    private readonly ITodoRepository _todoRepository;
    private readonly TodoHandler _handler;

    public CreateTodoHandlerTests()
    {
        _todoRepository = new Mock<ITodoRepository>().Object;
        _handler = new TodoHandler(_todoRepository);
    }


    [Fact]
    public void ShouldFailWhenInvalidCommand()
    {
        var invalidCommand = new CreateTodoCommand(
            String.Empty,
            String.Empty,
            DateTime.Now);
        var result = (GenericCommandResult) _handler.Handle(invalidCommand);
        
        Assert.False(_handler.IsValid);
        Assert.False(result.Success);
    }

    [Fact]
    public void ShouldSuccessWhenValidCommand()
    {
        var validCommand = new CreateTodoCommand(
            "Title",
            "User Valid",
            DateTime.Now);
        var result =  (GenericCommandResult) _handler.Handle(validCommand);
        
        Assert.True(_handler.IsValid);
        Assert.True(result.Success);
    }
}