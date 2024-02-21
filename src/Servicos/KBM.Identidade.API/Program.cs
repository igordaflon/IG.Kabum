using KBM.Identidade.API.Configuracoes;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath);
builder.Configuration.AddJsonFile("appsettings.json", true, true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true);
builder.Configuration.AddEnvironmentVariables();

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<StartupBase>();
}

builder.Services.AddApiConfiguration();

builder.Services.AddIdentityConfiguration(builder.Configuration);

builder.Services.AddSwaggerConfiguration();


var app = builder.Build();

app.UseSwaggerConfiguration();

app.UseApiConfiguration();

app.Run();
