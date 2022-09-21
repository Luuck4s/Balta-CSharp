using System;
using Todo.Domain.Commands;
using Xunit;

namespace Todo.Tests.Commands;

public class MakeTodoAsUndoneCommandTests
{
    private readonly MakeTodoAsUndoneCommand _invalidCommand;
    private readonly MakeTodoAsUndoneCommand _validCommand;

    public MakeTodoAsUndoneCommandTests()
    {
        _invalidCommand = new MakeTodoAsUndoneCommand(
            Guid.Empty, 
            string.Empty
        );
        _validCommand = new MakeTodoAsUndoneCommand(
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