using Microsoft.AspNetCore.Mvc;
using Todo.Data;
using Todo.Models;

namespace Todo.Controllers
{
  [ApiController]
  public class HomeController : ControllerBase
  {
    [HttpGet("/")]
    public IActionResult Get(
      [FromServices] AppDbContext context
    ) => Ok(context.Todos.ToList());


    [HttpPost("/")]
    public IActionResult Post(
      [FromBody] TodoModel todo,
      [FromServices] AppDbContext context
    )
    {
      context.Todos.Add(todo);
      context.SaveChanges();

      return Created($"/{todo.Id}",todo);
    }

    [HttpGet("/{id:int}")]
    public IActionResult GetById(
      [FromRoute] int id,
      [FromServices] AppDbContext context
    )
    {
      var todo = context.Todos.FirstOrDefault(todo => todo.Id == id);

      if(todo == null){
        return NotFound();
      }

      return Ok(todo);
    }

    [HttpPut("/{id:int}")]
    public IActionResult Put(
      [FromRoute] int id,
      [FromBody] TodoModel todo,
      [FromServices] AppDbContext context
    )
    {
      var todoModel = context.Todos.FirstOrDefault(todo => todo.Id == id);

      if (todoModel == null)
      {
        return NotFound();
      }

      todoModel.Title = todo.Title;
      todoModel.Done = todo.Done;

      context.Todos.Update(todoModel);
      context.SaveChanges();
      return Ok(todoModel);
    }


    [HttpDelete("/{id:int}")]
    public IActionResult Delete(
      [FromRoute] int id,
      [FromServices] AppDbContext context
    )
    {
      var todoModel = context.Todos.FirstOrDefault(todo => todo.Id == id);

      if (todoModel == null)
      {
        return NotFound();
      }

      context.Todos.Remove(todoModel);
      context.SaveChanges();
      return Ok(todoModel);
    }

  }
}