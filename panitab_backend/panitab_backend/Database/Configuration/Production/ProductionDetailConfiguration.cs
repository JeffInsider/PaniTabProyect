using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using panitab_backend.Database.Entities.Production;

namespace panitab_backend.Database.Configuration.Production
{
    public class ProductionDetailConfiguration : IEntityTypeConfiguration<ProductionDetailEntity>
    {
        public void Configure(EntityTypeBuilder<ProductionDetailEntity> builder)
        {
            builder.HasIndex(pd => new { pd.ProductionId, pd.BreadClassId });

            builder.Property(pd => pd.Status)
                   .HasConversion<string>()
                   .HasMaxLength(20);
        }
    }
}
