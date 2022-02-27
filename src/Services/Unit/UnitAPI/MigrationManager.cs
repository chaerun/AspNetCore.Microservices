﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace UnitAPI
{
  public static class MigrationManager
  {
    public static IHost MigrateDatabase<T>(this IHost host) where T : DbContext
    {
      using var scope = host.Services.CreateScope();
      using var appContext = scope.ServiceProvider.GetRequiredService<T>();
      appContext.Database.Migrate();

      return host;
    }
  }
}