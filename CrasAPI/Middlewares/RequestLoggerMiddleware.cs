using Microsoft.AspNetCore.Mvc.Controllers;
using Serilog;

namespace CrasAPI.Middlewares
{
    public class RequestLoggerMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLoggerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            var descriptor = endpoint?.Metadata.GetMetadata<ControllerActionDescriptor>();

            string controller = descriptor?.ControllerName ?? "Unknown";
            string action = descriptor?.ActionName ?? "Unknown";

            context.Request.EnableBuffering();
            using var reader = new StreamReader(context.Request.Body, leaveOpen: true);
            var requestBody = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;

            Log.ForContext("Controller", controller)
               .ForContext("Action", action)
               .Information(
                   "Request {Method} {Path} {Body}",
                   context.Request.Method,
                   context.Request.Path,
                   requestBody
               );

            var originalBody = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await _next(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseText = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            Log.ForContext("Controller", controller)
               .ForContext("Action", action)
               .Information(
                   "Response {StatusCode} {Body}",
                   context.Response.StatusCode,
                   responseText
               );

            await responseBody.CopyToAsync(originalBody);
        }
    }
}
