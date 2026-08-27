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

// Configure CORS policy to allow Angular application origin
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AngularApp", policy =>
    {
        // Target exact Angular development origin without trailing slash
        policy.WithOrigins("http://localhost:4200/")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    // Enable dark mode for the OpenAPI reference
    app.MapScalarApiReference(opt => opt.DarkMode = true);
}

// Enable CORS middleware using the configured policy
app.UseCors("AngularApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
