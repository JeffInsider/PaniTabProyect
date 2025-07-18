using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using panitab_backend.Database.Entities.Production;

namespace panitab_backend.Database.Configuration.Production
{
    public class MaterialConfiguration : IEntityTypeConfiguration<MaterialEntity>
    {
        public void Configure(EntityTypeBuilder<MaterialEntity> builder)
        {
            builder.Property(e => e.CurrentStock).HasPrecision(18, 2);
            builder.HasIndex(m => m.Name).IsUnique();
            builder.HasIndex(m => m.Category);
            builder.HasIndex(m => m.IsActive);
        }
    }
}
