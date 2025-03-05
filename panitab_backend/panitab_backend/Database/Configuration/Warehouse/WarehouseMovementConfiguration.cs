using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using panitab_backend.Database.Entities.Warehouse;

namespace panitab_backend.Database.Configuration.Warehouse
{
    public class WarehouseMovementConfiguration : IEntityTypeConfiguration<WarehouseMovementEntity>
    {
        public void Configure(EntityTypeBuilder<WarehouseMovementEntity> builder)
        {
            builder.HasOne(e => e.CreatedByUser)
                .WithMany()
                .HasForeignKey(e => e.CreatedBy)
                .HasPrincipalKey(e => e.Id)
                .IsRequired();

            builder.HasOne(e => e.UpdatedByUser)
                .WithMany()
                .HasForeignKey(e => e.UpdatedBy)
                .HasPrincipalKey(e => e.Id)
                .IsRequired();
        }
    }
}
