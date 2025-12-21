using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Controllers;
using CrasAPI.Data;
using CrasAPI.Repository.Interfaces;
using CrasAPI.Repository;
using CrasAPI.Services.Interfaces;
using CrasAPI.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("Default")
    )
);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information() 
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning) 
    .MinimumLevel.Override("System", Serilog.Events.LogEventLevel.Warning)
    .Enrich.FromLogContext() 
    .Enrich.WithEnvironmentName()
    .Enrich.WithThreadId()
    .WriteTo.File(
        path: "logs/CrasAPI-.log",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 31, 
        outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss}] [{Level:u3}] [{Controller}.{Action}] {Message:lj}{NewLine}{Exception}"
    )
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//  Middleware para logar cada requisição
app.Use(async (context, next) =>
{
    var endpoint = context.GetEndpoint();
    string controller = endpoint?.Metadata
        .GetMetadata<ControllerActionDescriptor>()?.ControllerName ?? "Unknown";

    controller += "Controller";

    string action = endpoint?.Metadata
        .GetMetadata<ControllerActionDescriptor>()?.ActionName ?? "UnknownAction";

    // Lendo request body
    context.Request.EnableBuffering();
    using var reader = new StreamReader(context.Request.Body, leaveOpen: true);
    var requestBody = await reader.ReadToEndAsync();
    context.Request.Body.Position = 0;
    requestBody = System.Text.RegularExpressions.Regex.Replace(requestBody, @"\s+", " ");

    Log.ForContext("Controller", controller)
       .ForContext("Action", action)
       .Information("Request: {Method} {Path} - QueryStringParams: {QueryString} - Body: {Body}", context.Request.Method, context.Request.Path, context.Request.QueryString.ToString(), requestBody);

    // Capturando response
    var originalBodyStream = context.Response.Body;
    using var responseBody = new MemoryStream();
    context.Response.Body = responseBody;

    await next();

    context.Response.Body.Seek(0, SeekOrigin.Begin);
    var responseText = await new StreamReader(context.Response.Body).ReadToEndAsync();
    context.Response.Body.Seek(0, SeekOrigin.Begin);
    responseText = System.Text.RegularExpressions.Regex.Replace(responseText, @"\s+", " ");

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

    string statusMessage = statusMessages.ContainsKey(context.Response.StatusCode)
    ? statusMessages[context.Response.StatusCode]
    : "Unknown";

    Log.ForContext("Controller", controller)
       .ForContext("Action", action)
       .Information("Response: {StatusCode} {StatusMessage} - Body: {Body}", context.Response.StatusCode, statusMessage, responseText);

    await responseBody.CopyToAsync(originalBodyStream);
});

app.MapControllers();

app.Run();
