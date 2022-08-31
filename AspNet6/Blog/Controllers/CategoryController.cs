using Blog.Data;
using Blog.Extensions;
using Blog.Models;
using Blog.ViewModels;
using Blog.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Blog.Controllers;

[ApiController]
public class CategoryController : ControllerBase
{

    private readonly IMemoryCache _cache;
    private readonly BlogDataContext _context;

    public CategoryController(BlogDataContext context, IMemoryCache cache)
    {
        _context = context;
        _cache = cache;
    }
    [HttpGet("v1/categories")]
    public async Task<IActionResult> GetAsync(
        )
    {
        var categories = _cache.GetOrCreate("CategoriesCache", entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
            return GetCategories();
        });
        return Ok(new ResultViewModel<List<Category>>(categories));
    }

    private List<Category> GetCategories()
    {
        return _context.Categories.ToList();
    }


    [HttpGet("v1/categories/{id:int}")]
    public async Task<IActionResult> GetByIdAsync(
        [FromRoute] int id
        )
    {
        var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);

        if (category is null)
        {
            return NotFound(new ResultViewModel<Category>("Category not found"));
        }

        return Ok(new ResultViewModel<Category>(category));
    }

    [HttpPost("v1/categories")]
    public async Task<IActionResult> PostAsync(
        [FromBody] EditorCategoryViewModel model
        )
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
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

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
        [FromBody] EditorCategoryViewModel model
        )
    {
        try
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (category is null)
            {
                return NotFound(new ResultViewModel<Category>("Category not found"));
            }

            category.Name = model.Name;
            category.Slug = model.Slug;

            _context.Categories.Update(category);
            await _context.SaveChangesAsync();

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
        [FromRoute] int id
        )
    {
        try
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (category is null)
            {
                return NotFound(new ResultViewModel<Category>("Category not found"));
            }


            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

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