using Microsoft.AspNetCore.Mvc.Controllers;
using Serilog;
using System.Text.RegularExpressions;

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
            controller += "Controller";

            string action = descriptor?.ActionName ?? "UnknownAction";

            // Request body
            context.Request.EnableBuffering();
            using var reader = new StreamReader(context.Request.Body, leaveOpen: true);
            var requestBody = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;
            requestBody = Regex.Replace(requestBody, @"\s+", " ");

            Log.ForContext("Controller", controller)
               .ForContext("Action", action)
               .Information(
                   "Request: {Method} {Path} - QueryStringParams: {QueryString} - Body: {Body}",
                   context.Request.Method,
                   context.Request.Path,
                   context.Request.QueryString.ToString(),
                   requestBody
               );

            // Capture response
            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await _next(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseText = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            responseText = Regex.Replace(responseText, @"\s+", " ");

            var statusMessages = new Dictionary<int, string>
            {
                {100, "Continue"},
                {101, "Switching Protocols"},
                {102, "Processing"},
                {103, "Early Hints"},
                {200, "OK"},
                {201, "Created"},
                {202, "Accepted"},
                {203, "Non-Authoritative Information"},
                {204, "No Content"},
                {205, "Reset Content"},
                {206, "Partial Content"},
                {207, "Multi-Status"},
                {208, "Already Reported"},
                {226, "IM Used"},
                {300, "Multiple Choices"},
                {301, "Moved Permanently"},
                {302, "Found"},
                {303, "See Other"},
                {304, "Not Modified"},
                {305, "Use Proxy"},
                {306, "Switch Proxy"},
                {307, "Temporary Redirect"},
                {308, "Permanent Redirect"},
                {400, "Bad Request"},
                {401, "Unauthorized"},
                {402, "Payment Required"},
                {403, "Forbidden"},
                {404, "Not Found"},
                {405, "Method Not Allowed"},
                {406, "Not Acceptable"},
                {407, "Proxy Authentication Required"},
                {408, "Request Timeout"},
                {409, "Conflict"},
                {410, "Gone"},
                {411, "Length Required"},
                {412, "Precondition Failed"},
                {413, "Payload Too Large"},
                {414, "URI Too Long"},
                {415, "Unsupported Media Type"},
                {416, "Range Not Satisfiable"},
                {417, "Expectation Failed"},
                {418, "I'm a teapot"},
                {421, "Misdirected Request"},
                {422, "Unprocessable Entity"},
                {423, "Locked"},
                {424, "Failed Dependency"},
                {425, "Too Early"},
                {426, "Upgrade Required"},
                {428, "Precondition Required"},
                {429, "Too Many Requests"},
                {431, "Request Header Fields Too Large"},
                {451, "Unavailable For Legal Reasons"},
                {500, "Internal Server Error"},
                {501, "Not Implemented"},
                {502, "Bad Gateway"},
                {503, "Service Unavailable"},
                {504, "Gateway Timeout"},
                {505, "HTTP Version Not Supported"},
                {506, "Variant Also Negotiates"},
                {507, "Insufficient Storage"},
                {508, "Loop Detected"},
                {510, "Not Extended"},
                {511, "Network Authentication Required"}
            };

            string statusMessage = statusMessages.TryGetValue(context.Response.StatusCode, out var msg)
                ? msg
                : "Unknown";

            Log.ForContext("Controller", controller)
               .ForContext("Action", action)
               .Information(
                   "Response: {StatusCode} {StatusMessage} - Body: {Body}",
                   context.Response.StatusCode,
                   statusMessage,
                   responseText
               );

            await responseBody.CopyToAsync(originalBodyStream);
        }
    }
}
