using Serilog;

//Configure early bootstrap logging (catches startup errors)
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting web application...");

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Host.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.Seq("http://localhost:5341") // This to write to Seq server, make sure you have Seq running on your machine or change the URL to your Seq server
    );

    builder.Services.AddControllers();
    // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
    builder.Services.AddOpenApi();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
        app.UseSwaggerUI(options =>
        {
            // Point Swagger UI to the native .NET 10 OpenAPI JSON endpoint
            options.SwaggerEndpoint("/openapi/v1.json", "v1");
        });
    }
    //This is handling the Request Logging for Serilog
    //app.UseSerilogRequestLogging(options =>
    //{
    //    options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
    //    {
    //        // Automatically attaches reqUser to every request log
    //        var user = httpContext.User.Identity?.Name ?? "Anonymous";
    //        diagnosticContext.Set("reqUser", user);
    //    };
    //});

    app.UseSerilogRequestLogging();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}