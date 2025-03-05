using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using panitab_backend.Database.Entities.Production;

namespace panitab_backend.Database.Configuration.Production
{
    public class BreadClassMaterialConfiguration : IEntityTypeConfiguration<BreadClassMaterialEntity>
    {
        public void Configure(EntityTypeBuilder<BreadClassMaterialEntity> builder)
        {
            builder.Property(e => e.QuantityUsed).HasPrecision(10, 2);
        }
    }
}
