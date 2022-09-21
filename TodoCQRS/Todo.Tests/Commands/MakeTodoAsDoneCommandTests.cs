using System;
using Todo.Domain.Commands;
using Xunit;

namespace Todo.Tests.Commands;

public class MakeTodoAsDoneCommandTests
{
    private readonly MakeTodoAsDoneCommand _invalidCommand;
    private readonly MakeTodoAsDoneCommand _validCommand;

    public MakeTodoAsDoneCommandTests()
    {
        _invalidCommand = new MakeTodoAsDoneCommand(
            Guid.Empty, 
            string.Empty
        );
        _validCommand = new MakeTodoAsDoneCommand(
            Guid.NewGuid(),
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