using Serilog;

namespace EventAPI.Extensions;

public static class LogSettings
{
  public static void AddLogSettings(this WebApplicationBuilder builder, string applicationName)
  {
    builder.Logging.ClearProviders();
    var logger = new LoggerConfiguration()
    .Enrich.WithProperty("ApplicationName", $"{applicationName} - {Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")}")
    .Enrich.WithEnvironmentName()
    .Enrich.WithMachineName()
    .Enrich.WithProcessId()
    .Enrich.WithThreadId()
    .Enrich.WithMemoryUsage()
    .Enrich.FromLogContext()
    .Enrich.WithCorrelationId()
    .Enrich.WithCorrelationIdHeader()
    .WriteTo.Console()
    .WriteTo.Seq("http://localhost:5341")
    .CreateLogger();
    builder.Logging.AddSerilog(logger);

  }
}