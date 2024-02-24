using KBM.WebApp.MVC.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityConfiguration();


var app = builder.Build();

app.UseMvcConfiguration();

app.Run();
