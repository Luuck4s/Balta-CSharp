namespace Todo.Api.Configuration;

public static class ConfigurationServiceCollectionExtensions
{
    public static void AddConfigurations(this WebApplication app)
    {
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
        
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
    }
}