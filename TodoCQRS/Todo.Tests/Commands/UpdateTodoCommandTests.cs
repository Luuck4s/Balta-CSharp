using System;
using Todo.Domain.Commands;
using Xunit;

namespace Todo.Tests.Commands;

public class UpdateTodoCommandTests
{
    private readonly UpdateTodoCommand _invalidCommand;
    private readonly UpdateTodoCommand _validCommand;

    public UpdateTodoCommandTests()
    {
        _invalidCommand = new UpdateTodoCommand(
            Guid.Empty, 
            string.Empty,
            string.Empty
        );
        _validCommand = new UpdateTodoCommand(
            Guid.NewGuid(), 
            "New title",
            "User Valid"
        );
    }

    [Fact]
    public void ShouldFailWhenInvalidCommand()
    {
        _invalidCommand.Validate();

        Assert.False(_invalidCommand.IsValid);
    }


    [Fact]
    public void ShouldSuccessWhenValidCommand()
    {
        _validCommand.Validate();

        Assert.True(_validCommand.IsValid);
    }
}