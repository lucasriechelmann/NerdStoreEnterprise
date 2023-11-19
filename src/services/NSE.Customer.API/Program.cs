using NSE.Customer.API.Configuration;
using NSE.WebAPI.Core.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

builder.Services.AddApiConfiguration(builder.Configuration);
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddSwaggerConfiguration();
builder.Services.RegisterServices();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwaggerConfiguration();
app.UseApiConfiguration(app.Environment);

app.Run();
