using Serilog;

namespace EventAPI.Extensions;

public static class LogSettings
{
  public static void AddLogSettings(this WebApplicationBuilder builder, string applicationName, ConfigurationManager configuration)
  {
    var seq = configuration["Settings:Seq"];
    if (string.IsNullOrWhiteSpace(seq)) throw new ArgumentNullException("Seq is required");
    Console.WriteLine($"Environment: {builder.Environment.EnvironmentName}");
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
    .WriteTo.Seq(seq)
    .CreateLogger();
    builder.Logging.AddSerilog(logger);

  }
}