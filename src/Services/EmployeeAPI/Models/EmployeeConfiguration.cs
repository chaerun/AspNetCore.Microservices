using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeAPI.Models
{
  public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
  {
    public virtual void Configure(EntityTypeBuilder<Employee> builder)
    {
      builder.HasKey(e => e.Id);
      builder.Property(e => e.Id).ValueGeneratedOnAdd();
      builder.Property(e => e.Name).HasMaxLength(100).IsRequired();
      builder.Property(e => e.UnitId).IsRequired();
      builder.Property(e => e.CreatedBy).IsRequired();
      builder.Property(e => e.CreatedAt).IsRequired();
      builder.Property(e => e.IsActive);

      builder.HasIndex(e => e.Name).IsUnique();
      builder.HasIndex(e => e.IsActive);

      // Global query filter for soft delete
      builder.HasQueryFilter(e => e.IsActive);
    }
  }
}