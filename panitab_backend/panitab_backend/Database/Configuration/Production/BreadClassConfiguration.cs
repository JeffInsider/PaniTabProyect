using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using panitab_backend.Database.Entities.Production;

namespace panitab_backend.Database.Configuration.Production
{
    public class BreadClassConfiguration : IEntityTypeConfiguration<BreadClassEntity>
    {
        public void Configure(EntityTypeBuilder<BreadClassEntity> builder)
        {
            builder.Property(e => e.TraysPerQuintal).HasPrecision(18, 2);
            builder.Property(e => e.BreadsPerTray).HasPrecision(18, 2);
            builder.Property(e => e.PricePerQuintal).HasPrecision(18, 2);
            builder.Property(e => e.PackagingPrice).HasPrecision(18, 2);
            builder.Property(e => e.CustomerPrice).HasPrecision(18, 2);
            builder.Property(e => e.PublicPrice).HasPrecision(18, 2);

            builder.HasIndex(bc => bc.Name).IsUnique();
            builder.HasIndex(bc => bc.Category);
            builder.HasIndex(bc => bc.IsActive);
        }
    }
}
