using NSE.WebApp.MVC.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityConfiguration();
builder.Services.AddMVCConfiguration();
builder.Services.RegisterServices();

var app = builder.Build();

app.UseMVCConfiguration(builder.Environment);

app.Run();
