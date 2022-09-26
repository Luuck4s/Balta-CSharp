using Microsoft.EntityFrameworkCore;
using Todo.Api.Settings;
using Todo.Infra.Contexts;

namespace Todo.Api.Configuration;

public static class DatabaseServiceCollectionExtensions
{
    public static void AddDatabaseServices(this IServiceCollection services,  ConfigurationManager configuration)
    {
        var databaseSettings = configuration.GetSection(nameof(DatabaseSettings)).Get<DatabaseSettings>();

        if (databaseSettings.InMemory)
        {
            services.AddDbContext<DataContext>(
                opt =>
                    opt.UseInMemoryDatabase("Database")
            );    
        }
        else
        {
            services.AddDbContext<DataContext>(
                opt =>
                    opt.UseSqlServer(databaseSettings.ConnectionString)
            );  
        }


    }
}