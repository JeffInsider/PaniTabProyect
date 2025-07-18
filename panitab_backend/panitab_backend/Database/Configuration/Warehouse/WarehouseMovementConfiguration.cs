using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using panitab_backend.Database.Entities.Warehouse;

namespace panitab_backend.Database.Configuration.Warehouse
{
    public class WarehouseMovementConfiguration : IEntityTypeConfiguration<WarehouseMovementEntity>
    {
        public void Configure(EntityTypeBuilder<WarehouseMovementEntity> builder)
        {
            builder.HasIndex(wm => wm.MovementNumber).IsUnique();
            builder.HasIndex(wm => wm.MovementDate);
        }
    }
}
