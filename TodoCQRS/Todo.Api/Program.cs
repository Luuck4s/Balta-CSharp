using Todo.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddDomainServices();
builder.Services.AddDatabaseServices();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.AddConfigurations();

app.Run();
