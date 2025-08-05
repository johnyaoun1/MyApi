using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using MediatR;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MyApi.Data;
// using MyApi.Services; // Removed because Services namespace does not exist
// using MyApi.Middleware; // Removed because Middleware namespace does not exist

var builder = WebApplication.CreateBuilder(args);

// Register services to the container
builder.Services.AddControllers();

// Register the DbContext with dependency injection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));  // Adjust connection string as needed

// Register your DepartmentService for dependency injection
builder.Services.AddScoped<IDepartmentService, DepartmentService>();

// Register MediatR
builder.Services.AddMediatR(typeof(Program));

// Register Swagger/OpenAPI services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers(options =>
{
    // Register the action filter globally
    options.Filters.Add<MyActionFilter>();
});

// Read AppSettings from configuration
var appSettings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();

var app = builder.Build();

// Global Exception Handling
app.UseExceptionHandler(app =>
{
    app.Run(async context =>
    {
        var exceptionHandlerPathFeature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature>();
        if (exceptionHandlerPathFeature != null)
        {
            var exception = exceptionHandlerPathFeature.Error;
            var response = context.Response;
            response.ContentType = "application/json";

            if (exception is DepartmentNotFoundException)
            {
                response.StatusCode = 404;
                await response.WriteAsJsonAsync(new { message = exception.Message });
            }
            else
            {
                response.StatusCode = 500;
                await response.WriteAsJsonAsync(new { message = "An unexpected error occurred." });
            }
        }
    });
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();
app.UseMiddleware<LoggingMiddleware>();
app.Run();

public class Department
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Department name is required")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Department name must be between 3 and 100 characters")]
    public string? Name { get; set; }
}

public class DepartmentNotFoundException : Exception
{
    public DepartmentNotFoundException(string message) : base(message) { }
}

public class AppSettings
{
    public string? ApiBaseUrl { get; set; }
}