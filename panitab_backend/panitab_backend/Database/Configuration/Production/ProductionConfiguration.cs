using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using panitab_backend.Database.Entities.Production;

namespace panitab_backend.Database.Configuration.Production
{
    public class ProductionConfiguration : IEntityTypeConfiguration<ProductionEntity>
    {
        public void Configure(EntityTypeBuilder<ProductionEntity> builder)
        {
            builder.HasIndex(p => p.ProductionNumber).IsUnique();
            builder.HasIndex(p => p.ProductionDate);

            builder.Property(p => p.Status)
                   .HasConversion<string>()
                   .HasMaxLength(20);
        }
    }
}
