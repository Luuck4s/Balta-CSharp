using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Todo.Api.Configuration;

public static class AuthServiceCollectionExtensions
{
    public static void AddAuthConfigurations(this IServiceCollection service)
    {
        service
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = "https://securetoken.google.com/balta-todo-4cb6a";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "https://securetoken.google.com/balta-todo-4cb6a",
                    ValidateAudience = true,
                    ValidAudience = "balta-todo-4cb6a",
                    ValidateLifetime = true
                };
            });
    }
}