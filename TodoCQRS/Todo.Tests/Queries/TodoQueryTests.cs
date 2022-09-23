using System;
using System.Collections.Generic;
using System.Linq;
using Todo.Domain.Entities;
using Todo.Domain.Queries;
using Xunit;

namespace Todo.Tests.Queries;

public class TodoQueryTests
{
    private List<TodoItem> _items;

    public TodoQueryTests()
    {
        _items = new List<TodoItem>()
        {
            new("Task 1", DateTime.Now, "User A"),
            new("Task 2", DateTime.Now, "User A"),
            new("Task 3", DateTime.Now, "User B"),
            new("Task 4", DateTime.Now, "User B"),
            new("Task 5", DateTime.Now, "User C"),
        };
    }

    [Fact]
    public void ShouldReturnToDosOnlyFromInformedUser()
    {
        var result = _items
            .AsQueryable()
            .Where(TodoQueries.GetAll("User A"));
        
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }
}