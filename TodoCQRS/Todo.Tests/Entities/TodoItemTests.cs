using System;
using Todo.Domain.Entities;
using Xunit;

namespace Todo.Tests.Entities;

public class TodoItemTests
{

    private readonly TodoItem _todo;
    public TodoItemTests()
    {
        _todo = new TodoItem(
            "User",
            DateTime.Now,
            "Valid user");
    }
    
    [Fact]
    public void DoneShouldFalseWhenCreateNewTodoItem()
    {
        Assert.False(_todo.Done);
    }
}