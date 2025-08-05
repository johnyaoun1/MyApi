using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MyApi.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            // Log request
            Console.WriteLine("Hello incoming requesting");
            await _next(httpContext);
        }
    }
}
