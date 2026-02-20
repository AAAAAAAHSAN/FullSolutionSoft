using FullSolutionSoft.Application.Interfaces;
using FullSolutionSoft.Application.Services;
using FullSolutionSoft.Infrastructure.Data; // your DbContext namespace
using FullSolutionSoft.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ----------------------
// Add services to the container
// ----------------------

// Add Controllers
builder.Services.AddControllers();

// Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext (Infrastructure)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Default")
    )
);

// Application layer services
builder.Services.AddScoped<ICustomerService, CustomerService>();
// Infrastructure layer repositories
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

var app = builder.Build();

// ----------------------
// Configure Middleware
// ----------------------

// Enable Swagger in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "FullSolutionSoft API v1");
        c.RoutePrefix = string.Empty; // Swagger will open at root: http://localhost:5000/
    });
}

// Enable HTTPS redirection
app.UseHttpsRedirection();

// Authorization middleware
app.UseAuthorization();

// Map Controllers
app.MapControllers();

// Optional: Test endpoint (WeatherForecast)
app.MapGet("/weatherforecast", () =>
{
    var summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

// Run the app
app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}