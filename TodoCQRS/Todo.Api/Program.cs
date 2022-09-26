using Todo.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddDomainServices();
builder.Services.AddDatabaseServices(builder.Configuration);
builder.Services.AddAuthConfigurations();
builder.Services.AddSwaggerService();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI();
app.AddConfigurations();

app.Run();
