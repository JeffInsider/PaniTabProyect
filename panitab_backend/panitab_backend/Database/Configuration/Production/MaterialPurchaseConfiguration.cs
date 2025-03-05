using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using panitab_backend.Database.Entities.Production;

namespace panitab_backend.Database.Configuration.Production
{
    public class MaterialPurchaseConfiguration : IEntityTypeConfiguration<MaterialPurchaseEntity>
    {
        public void Configure(EntityTypeBuilder<MaterialPurchaseEntity> builder)
        {
            builder.Property(e => e.UnitPrice).HasPrecision(10, 2);
            builder.Property(e => e.QuantityPurchased).HasPrecision(10, 2);
            builder.Property(e => e.Balance).HasPrecision(10, 2);
        }
    }
}
