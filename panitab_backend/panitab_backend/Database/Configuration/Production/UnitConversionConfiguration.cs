using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using panitab_backend.Database.Entities.Production;

namespace panitab_backend.Database.Configuration.Production
{
    public class UnitConversionConfiguration : IEntityTypeConfiguration<UnitConversionEntity>
    {
        public void Configure(EntityTypeBuilder<UnitConversionEntity> builder)
        {
            builder.Property(e => e.ConversionFactor).HasPrecision(10, 2);
        }
    }
}
