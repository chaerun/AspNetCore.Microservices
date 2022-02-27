using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using UnitAPI.Models;

namespace UnitAPI
{
  public class Program
  {
    public static void Main(string[] args)
    {
      CreateHostBuilder(args)
        .Build()
        .MigrateDatabase<ApplicationDbContext>()
        .Run();

    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
              webBuilder.UseStartup<Startup>();
            });
  }
}
