using System.Text.RegularExpressions;
using Blog.Data;
using Blog.Extensions;
using Blog.Models;
using Blog.Services;
using Blog.ViewModels;
using Blog.ViewModels.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;

namespace Blog.Controllers;

[ApiController]
public class AccountController : ControllerBase
{
    private readonly BlogDataContext _context;
    private readonly TokenService _tokenService;
    private readonly EmailService _emailService;

    public AccountController(TokenService tokenService, BlogDataContext blogDataContext, EmailService emailService)
    {
        _tokenService = tokenService;
        _context = blogDataContext;
        _emailService = emailService;
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

            _emailService.Send(user.Name, user.Email, "Bem vindo ao blog", $"Sua senha é {password}");

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
    
    [Authorize]
    [HttpPost("v1/accounts/upload-image")]
    public async Task<IActionResult> UploadImage(
        [FromBody] UploadImageViewModel model)
    {
        var fileName = $"{Guid.NewGuid().ToString()}.jpg";
        var data = new Regex(@"data:image\/[a-z]+;base64,").Replace(model.Base64Image, "");
        var bytes = Convert.FromBase64String(data);

        try
        {
            await System.IO.File.WriteAllBytesAsync($"wwwroot/images/{fileName}", bytes);
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<string>("05X04 - Error on save image"));
        }
        
        var user = await _context.Users
            .AsNoTracking()
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Name == User.Identity.Name);

        if (user == null)
        {
            return StatusCode(404, new ResultViewModel<string>("User not found"));
        }

        user.Image = $"https://localhost:0000/images/${fileName}";

        try
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<string>("05X05 - Error on save image"));
        }

        return Ok(new ResultViewModel<string>("Image updated"));
    }

}