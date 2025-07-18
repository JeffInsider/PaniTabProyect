using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using panitab_backend.Database.Entities.Warehouse;

namespace panitab_backend.Database.Configuration.Warehouse
{
    public class WarehouseControlDetailConfiguration : IEntityTypeConfiguration<WarehouseControlDetailEntity>
    {
        public void Configure(EntityTypeBuilder<WarehouseControlDetailEntity> builder)
        {
            builder.HasIndex(wcd => new { wcd.WarehouseControlId, wcd.BreadClassId });
        }
    }
}
