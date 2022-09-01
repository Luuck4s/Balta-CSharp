using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

[ApiController]
[Route("")]
public class HomeController: ControllerBase
{
    private readonly IConfiguration _configuration;
    public HomeController(
        IConfiguration configuration)
    {
        _configuration = configuration;
    }
    [HttpGet("")]
    public IActionResult Get()
    {
        var env = _configuration.GetValue<string>("Env");
        return Ok(new
        {
            environment = env
        });
    }
    
    
}