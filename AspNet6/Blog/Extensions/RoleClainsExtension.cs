using System.Security.Claims;
using Blog.Models;

namespace Blog.Extensions;

public static class RoleClainsExtension
{
    public static IEnumerable<Claim> GetClaims(this User user)
    {
        var results = new List<Claim>
        {
            new(ClaimTypes.Name, user.Name)
        };
        results.AddRange(
            user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Slug))
        );

        return results;
    }
}