using Blog.Data;
using Blog.Extensions;
using Blog.Models;
using Blog.Services;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;

namespace Blog.Controllers;

[ApiController]
public class AccountController : ControllerBase
{
    private readonly BlogDataContext _context;
    private readonly TokenService _tokenService;

    public AccountController(TokenService tokenService, BlogDataContext blogDataContext)
    {
        _tokenService = tokenService;
        _context = blogDataContext;
    }

    [HttpPost("v1/accounts/")]
    public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));
        }

        var user = new User
        {
            Name = model.Name,
            Email = model.Email,
            Bio = String.Empty,
            Image = String.Empty,
            Slug = model.Email.Replace("@", "-").Replace(".", "_")
        };

        var password = PasswordGenerator.Generate(length: 25);
        user.PasswordHash = PasswordHasher.Hash(password);

        try
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return Ok(new ResultViewModel<dynamic>(new
            {
                user = user.Email, password
            }));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<string>("Internal Server Error"));
        }
    }

    [HttpPost("v1/accounts/login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));
        }

        var user = await _context.Users
            .AsNoTracking()
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Email == model.Email);

        if (user == null)
        {
            return StatusCode(401, new ResultViewModel<string>("User or password invalid"));
        }

        if (!PasswordHasher.Verify(user.PasswordHash, model.Password))
        {
            return StatusCode(401, new ResultViewModel<string>("User or password invalid"));
        }

        try
        {
            var token = _tokenService.GenerateToken(user);

            return Ok(new ResultViewModel<string>(token, errors: null));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<string>("Internal Server Error"));
        }
    }
}