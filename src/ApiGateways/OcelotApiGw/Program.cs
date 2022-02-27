using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace OcelotApiGw
{
  public class Program
  {
    public static void Main(string[] args)
    {
      Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .CreateBootstrapLogger();

      CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((context, config) =>
        {
          config.AddJsonFile($"ocelot.{context.HostingEnvironment.EnvironmentName}.json", true, true);
        })
        .UseSerilog((context, services, configuration) => configuration
          .ReadFrom.Configuration(context.Configuration)
          .ReadFrom.Services(services))
        .ConfigureWebHostDefaults(webBuilder =>
        {
          webBuilder.UseStartup<Startup>();
        });
  }
}
