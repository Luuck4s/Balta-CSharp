using Blog.Data;
using Blog.Extensions;
using Blog.Models;
using Blog.ViewModels;
using Blog.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers;

[ApiController]
public class CategoryController : ControllerBase
{
    [HttpGet("v1/categories")]
    public async Task<IActionResult> GetAsync(
        [FromServices] BlogDataContext context)
    {
        var categories = await context.Categories.ToListAsync();
        return Ok(new ResultViewModel<List<Category>>(categories));
    }


    [HttpGet("v1/categories/{id:int}")]
    public async Task<IActionResult> GetByIdAsync(
        [FromRoute] int id,
        [FromServices] BlogDataContext context)
    {
        var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

        if (category is null)
        {
            return NotFound(new ResultViewModel<Category>("Category not found"));
        }

        return Ok(new ResultViewModel<Category>(category));
    }

    [HttpPost("v1/categories")]
    public async Task<IActionResult> PostAsync(
        [FromBody] EditorCategoryViewModel model,
        [FromServices] BlogDataContext context)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest( new ResultViewModel<Category>(ModelState.GetErrors()));
        }
        try
        {
            var category = new Category()
            {
                Id = 0,
                Name = model.Name,
                Slug = model.Slug.ToLower(),
            };
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();

            return Created($"v1/categories/{category.Id}",  new ResultViewModel<Category>(category));
        }
        catch (DbUpdateException e)
        {
            return StatusCode(500, new ResultViewModel<Category>("05XE9 - Error on insert category"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Category>("05X10 - Internal Error"));
        }
    }

    [HttpPut("v1/categories/{id:int}")]
    public async Task<IActionResult> PutAsync(
        [FromRoute] int id,
        [FromBody] EditorCategoryViewModel model,
        [FromServices] BlogDataContext context)
    {
        try
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (category is null)
            {
                return NotFound(new ResultViewModel<Category>("Category not found"));
            }

            category.Name = model.Name;
            category.Slug = model.Slug;

            context.Categories.Update(category);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<Category>(category));
        }
        catch (DbUpdateException e)
        {
            return StatusCode(500, new ResultViewModel<Category>("05XE9 - Error on update category"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResultViewModel<Category>("05X11 - Internal Error"));
        }
    }

    [HttpDelete("v1/categories/{id:int}")]
    public async Task<IActionResult> PutAsync(
        [FromRoute] int id,
        [FromServices] BlogDataContext context)
    {
        try
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (category is null)
            {
                return NotFound(new ResultViewModel<Category>("Category not found"));
            }


            context.Categories.Remove(category);
            await context.SaveChangesAsync();

            return NoContent();
        }
        catch (DbUpdateException e)
        {
            return StatusCode(500, new ResultViewModel<Category>("05XE9 - Error on delete category"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResultViewModel<Category>("05X12 - Internal Error"));
        }
    }
}