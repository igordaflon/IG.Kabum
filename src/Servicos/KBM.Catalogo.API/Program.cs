using KBM.Catalogo.API.Data;
using KBM.Catalogo.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<CatalogoContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("ConnectionString"), ServerVersion.Parse("8.2.0")));

builder.Services.AddScoped<IProdutoRepository, IProdutoRepository>();
builder.Services.AddScoped<CatalogoContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
