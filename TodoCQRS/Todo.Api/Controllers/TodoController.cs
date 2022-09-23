using Microsoft.AspNetCore.Mvc;
using Todo.Domain.Commands;
using Todo.Domain.Entities;
using Todo.Domain.Handlers;
using Todo.Domain.Repositories;

namespace Todo.Api.Controllers;

[ApiController]
[Route("v1/todo")]
public class TodoController : ControllerBase
{
    private readonly TodoHandler _handler;
    private readonly ITodoRepository _repository;

    public TodoController(TodoHandler handler, ITodoRepository repository)
    {
        _handler = handler;
        _repository = repository;
    }

    [HttpPost("")]
    public GenericCommandResult Create(
        CreateTodoCommand command)
    {
        command.User = "Lucas 123";
        return (GenericCommandResult)_handler.Handle(command);
    }
    
    [HttpPut("")]
    public GenericCommandResult Update(
        UpdateTodoCommand command)
    {
        command.User = "Lucas 123";
        return (GenericCommandResult)_handler.Handle(command);
    }
    
    [HttpPut("mark-as-done")]
    public GenericCommandResult MarkAsDone(
        MakeTodoAsDoneCommand command)
    {
        command.User = "Lucas 123";
        return (GenericCommandResult)_handler.Handle(command);
    }
    
    [HttpPut("mark-as-undone")]
    public GenericCommandResult MarkAsUndone(
        MakeTodoAsUndoneCommand command)
    {
        command.User = "Lucas 123";
        return (GenericCommandResult)_handler.Handle(command);
    }

    [HttpGet("done")]
    public IEnumerable<TodoItem> GetAllDone()
    {
        return _repository.GetAllDone("Lucas 123");
    }

    [HttpGet("undone")]
    public IEnumerable<TodoItem> GetAllUndone()
    {
        return _repository.GetAllUndone("Lucas 123");
    }

    [HttpGet("period")]
    public IEnumerable<TodoItem> GetPeriod(
        [FromQuery] bool done = false,
        [FromQuery] DateTime date = default)
    {
        return _repository.GetAllPeriod(
            "Lucas 123",
            date,
            done
        );
    }
    
    
    [HttpGet("")]
    public IEnumerable<TodoItem> GetAll()
    {
        return _repository.GetAll("Lucas 123");
    }
}