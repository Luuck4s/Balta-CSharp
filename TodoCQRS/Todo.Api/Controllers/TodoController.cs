using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Domain.Commands;
using Todo.Domain.Entities;
using Todo.Domain.Handlers;
using Todo.Domain.Repositories;

namespace Todo.Api.Controllers;

[ApiController]
[Route("v1/todo")]
[Authorize]
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
        var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value!;
        command.User = user;
        return (GenericCommandResult)_handler.Handle(command);
    }
    
    [HttpPut("")]
    public GenericCommandResult Update(
        UpdateTodoCommand command)
    {
        var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value!;
        command.User = user;
        return (GenericCommandResult)_handler.Handle(command);
    }
    
    [HttpPut("mark-as-done")]
    public GenericCommandResult MarkAsDone(
        MakeTodoAsDoneCommand command)
    {
        var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value!;
        command.User = user;
        return (GenericCommandResult)_handler.Handle(command);
    }
    
    [HttpPut("mark-as-undone")]
    public GenericCommandResult MarkAsUndone(
        MakeTodoAsUndoneCommand command)
    {
        var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value!;
        command.User = user;
        return (GenericCommandResult)_handler.Handle(command);
    }

    [HttpGet("done")]
    public IEnumerable<TodoItem> GetAllDone()
    {
        var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value!;
        return _repository.GetAllDone(user);
    }

    [HttpGet("undone")]
    public IEnumerable<TodoItem> GetAllUndone()
    {
        var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value!;
        return _repository.GetAllUndone(user);
    }

    [HttpGet("period")]
    public IEnumerable<TodoItem> GetPeriod(
        [FromQuery] bool done = false,
        [FromQuery] DateTime date = default)
    {
        var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value!;
        return _repository.GetAllPeriod(
            user,
            date,
            done
        );
    }
    
    
    [HttpGet("")]
    public IEnumerable<TodoItem> GetAll()
    {
        var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value!;
        return _repository.GetAll(user);
    }
}