using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using panitab_backend.Database.Entities.Warehouse;

namespace panitab_backend.Database.Configuration.Warehouse
{
    public class WarehouseMovementDetailConfiguration : IEntityTypeConfiguration<WarehouseMovementDetailEntity>
    {
        public void Configure(EntityTypeBuilder<WarehouseMovementDetailEntity> builder)
        {
            builder.HasIndex(wmd => new { wmd.WarehouseMovementId, wmd.BreadClassId });

            builder.Property(wmd => wmd.MovementType)
                   .HasConversion<string>()
                   .HasMaxLength(20);
        }
    }
}
