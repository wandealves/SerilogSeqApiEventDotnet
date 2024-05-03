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
    var logger = new LoggerConfiguration();
    logger.Enrich.WithProperty("ApplicationName", $"{applicationName} - {Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")}")
    .MinimumLevel.Information()
    .Enrich.WithEnvironmentName()
    .Enrich.WithMachineName()
    .Enrich.WithProcessId()
    .Enrich.WithThreadId()
    .Enrich.WithMemoryUsage()
    .Enrich.FromLogContext()
    .Enrich.WithCorrelationId()
    .Enrich.WithCorrelationIdHeader();
    if (builder.Environment.EnvironmentName == "Development")
    {
      logger.WriteTo.Console(outputTemplate: "{Timestamp:yyy-MM-dd HH:mm:ss.fff} [{Level}] {Message}{NewLine}{Exception}");
    }
    logger.WriteTo.Seq(seq, restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information);
    builder.Logging.AddSerilog(logger.CreateLogger());

  }
}