using SalesWebApi.Business;
using Microsoft.EntityFrameworkCore;
using SalesWebApi.Data;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(connection));

// Register business service for dependency injection
builder.Services.AddScoped<SaleBusiness>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    // Enable dark mode for the OpenAPI reference
    app.MapScalarApiReference(opt => opt.DarkMode = true);
}

app.UseAuthorization();

app.MapControllers();

app.Run();
