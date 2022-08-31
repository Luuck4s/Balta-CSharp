using System.Text;
using Blog;
using Blog.Data;
using Blog.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);
ConfigureAuthentication(builder);
ConfigureMvc(builder);
ConfigureServices(builder);


var app = builder.Build();
LoadConfiguration(app);

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseStaticFiles();
app.Run();


void LoadConfiguration(WebApplication application)
{
    Configuration.JwtKey = application.Configuration.GetValue<string>("JwtKey");
    Configuration.ApiKeyName = application.Configuration.GetValue<string>("ApiKeyName");
    Configuration.ApiKey = application.Configuration.GetValue<string>("ApiKey");

    var smtp = new Configuration.SmtpConfiguration();
    application.Configuration.GetSection("Smtp").Bind(smtp);
    Configuration.Smtp = smtp;
}

void ConfigureAuthentication(WebApplicationBuilder builderApplication)
{
    var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
    builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(x =>
    {
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
}

void ConfigureMvc(WebApplicationBuilder builderApplication)
{
    builderApplication.Services.AddControllers()
        .ConfigureApiBehaviorOptions(options => { options.SuppressModelStateInvalidFilter = true; });
    builderApplication.Services.AddDbContext<BlogDataContext>();
}

void ConfigureServices(WebApplicationBuilder builderApplication)
{
    // builderApplication.Services.AddTransient(); sempre cria uma nova instância 
    // builderApplication.Services.AddScoped(); dura durante uma requisição após isso é finalizado 
    // builderApplication.Services.AddSingleton(); 1 para toda a aplicação, padrão singleton 

    builderApplication.Services.AddTransient<TokenService>();
    builderApplication.Services.AddTransient<EmailService>();
}