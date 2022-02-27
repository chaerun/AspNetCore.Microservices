using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace UnitAPI.Models
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Unit> Units { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override int SaveChanges()
    {
      UpdateCreatedAtAndSoftDelete();
      return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
      UpdateCreatedAtAndSoftDelete();
      return base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateCreatedAtAndSoftDelete()
    {
      var entries = ChangeTracker.Entries();

      var now = DateTime.Now;
      foreach (var entry in entries)
      {
        switch (entry.State)
        {
          case EntityState.Added:
            entry.CurrentValues["CreatedAt"] = now;
            entry.CurrentValues["CreatedBy"] = "System";
            entry.CurrentValues["IsActive"] = true;
            break;
          case EntityState.Deleted:
            entry.State = EntityState.Modified;
            entry.CurrentValues["IsActive"] = false;
            break;
        }
      }
    }
  }
}
