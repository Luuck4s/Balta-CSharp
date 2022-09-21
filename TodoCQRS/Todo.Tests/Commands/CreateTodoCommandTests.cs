using System;
using Todo.Domain.Commands;
using Xunit;

namespace Todo.Tests.Commands;

public class CreateTodoCommandTests
{
    private readonly CreateTodoCommand _invalidCommand;
    private readonly CreateTodoCommand _validCommand;

    public CreateTodoCommandTests()
    {
        _invalidCommand = new CreateTodoCommand(
            string.Empty,
            string.Empty,
            DateTime.Now
        );
        _validCommand = new CreateTodoCommand(
            "Task",
            "User Valid",
            DateTime.Now
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