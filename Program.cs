using Microsoft.EntityFrameworkCore;
using MyApi.Data;
using MyApi.Middleware;
using MyApi.Filters;
using MyApi.Services;
using MediatR;
using MyApi.Commands;

var builder = WebApplication.CreateBuilder(args);

// Register services in the container
builder.Services.AddControllers(options =>
{
    options.Filters.Add<CallerValidationFilter>();
});

// Register StudentService as Singleton
builder.Services.AddSingleton<StudentService>();

// Register the ApplicationDbContext with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register MediatR for CQRS
builder.Services.AddMediatR(typeof(Program));

// Register Middleware
builder.Services.AddScoped<CallerValidationFilter>();

// Register necessary filters globally
builder.Services.AddScoped<CallerValidationFilter>();

// Configure Dependency Injection for services
builder.Services.AddScoped<LoggingMiddleware>();

var app = builder.Build();

// Apply filters globally
// CallerValidationFilter is now registered globally via AddControllers options above.

// Apply filters globally
// CallerValidationFilter is now registered globally via AddControllers options above.

// Use default routing for controllers
app.MapControllers();

// Run the application
app.Run();
