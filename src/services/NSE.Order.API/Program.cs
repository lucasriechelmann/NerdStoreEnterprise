using NSE.Order.API.Configuration;
using NSE.WebAPI.Core.Extensions;
using NSE.WebAPI.Core.Identity;
var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddAppSettingsJsonConfiguration(builder.Environment);
builder.Services.AddApiConfiguration(builder.Configuration);
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddSwaggerConfiguration();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.RegisterServices();
builder.Services.AddMessageBusConfiguration(builder.Configuration);

var app = builder.Build();

app.UseSwaggerConfiguration();
app.UseApiConfiguration(app.Environment);

app.Run();
