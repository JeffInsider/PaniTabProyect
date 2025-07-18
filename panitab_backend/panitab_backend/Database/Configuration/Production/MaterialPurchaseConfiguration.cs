using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using panitab_backend.Database.Entities.Production;

namespace panitab_backend.Database.Configuration.Production
{
    public class MaterialPurchaseConfiguration : IEntityTypeConfiguration<MaterialPurchaseEntity>
    {
        public void Configure(EntityTypeBuilder<MaterialPurchaseEntity> builder)
        {
            builder.Property(e => e.UnitPrice).HasPrecision(18, 2);
            builder.Property(e => e.QuantityPurchased).HasPrecision(18, 2);
            builder.Property(e => e.Balance).HasPrecision(18, 2);

            builder.HasIndex(mp => mp.PurchaseDate);
            builder.HasIndex(mp => mp.SupplierId);
            builder.HasIndex(mp => mp.MaterialId);

            builder.Property(mp => mp.PurchaseStatus)
                   .HasConversion<string>()
                   .HasMaxLength(20);
        }
    }
}
