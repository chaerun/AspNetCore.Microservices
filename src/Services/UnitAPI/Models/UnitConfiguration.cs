using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UnitAPI.Models
{
  public class UnitConfiguration : IEntityTypeConfiguration<Unit>
  {
    public virtual void Configure(EntityTypeBuilder<Unit> builder)
    {
      builder.HasKey(e => e.Id);
      builder.Property(e => e.Id).ValueGeneratedOnAdd();
      builder.Property(e => e.Name).HasMaxLength(100).IsRequired();
      builder.Property(e => e.Code).HasMaxLength(100).IsRequired();
      builder.Property(e => e.CreatedBy).IsRequired();
      builder.Property(e => e.CreatedAt).IsRequired();
      builder.Property(e => e.IsActive);

      builder.HasIndex(e => e.IsActive);

      // Global query filter for soft delete
      builder.HasQueryFilter(e => e.IsActive);
    }
  }
}