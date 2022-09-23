using Todo.Domain.Handlers;
using Todo.Domain.Repositories;
using Todo.Infra.Repositories;

namespace Todo.Api.Configuration;

public static class DomainServiceCollectionExtensions
{
    public static void AddDomainServices(this IServiceCollection services)
    {
        services.AddTransient<ITodoRepository, TodoRepository>();
        services.AddTransient<TodoHandler, TodoHandler>();
    }
}