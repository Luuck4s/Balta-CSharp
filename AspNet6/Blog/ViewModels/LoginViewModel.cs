using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels;

public class LoginViewModel
{
    [Required]
    public string Password { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}