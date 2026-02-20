using FullSolutionSoft.Application.Common;
using FullSolutionSoft.Application.Interfaces;
using FullSolutionSoft.Application.Services;
using FullSolutionSoft.Infrastructure.Data; // your DbContext namespace
using FullSolutionSoft.Infrastructure.Repositories;
using FullSolutionSoft.Infrastructure.Secutiry;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
builder.Services.AddScoped<IOrderService, OrderService>();

// Infrastructure layer repositories
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

// ----------------------
// Configure Middleware
// ----------------------

// Load JWT settings
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
var key = Encoding.UTF8.GetBytes(jwtSettings.Secret);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddAuthorization();
builder.Services.AddControllers();

//builder.Services.AddSwaggerGen(options =>
//{
//    options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
//    {
//        Type = SecuritySchemeType.Http,
//        Scheme = "bearer",
//        BearerFormat = "JWT",
//        Description = "JWT Authorization header using the Bearer scheme."
//    });
//    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
//    {
//        [new OpenApiSecuritySchemeReference("bearer", document)] = []
//    });
//});

var app = builder.Build();
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