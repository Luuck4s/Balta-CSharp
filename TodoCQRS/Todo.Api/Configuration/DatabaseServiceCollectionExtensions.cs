using Microsoft.EntityFrameworkCore;
using Todo.Infra.Contexts;

namespace Todo.Api.Configuration;

public static class DatabaseServiceCollectionExtensions
{
    public static void AddDatabaseServices(this IServiceCollection services)
    {
        services.AddDbContext<DataContext>(
            opt =>
                opt.UseInMemoryDatabase("Database")
        );
    }
}