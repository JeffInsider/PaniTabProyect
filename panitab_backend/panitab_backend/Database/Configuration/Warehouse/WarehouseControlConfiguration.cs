using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using panitab_backend.Database.Entities.Warehouse;

namespace panitab_backend.Database.Configuration.Warehouse
{
    public class WarehouseControlConfiguration : IEntityTypeConfiguration<WarehouseControlEntity>
    {
        public void Configure(EntityTypeBuilder<WarehouseControlEntity> builder)
        {
            // Aqui se establece que el numero de control es unico
            builder.HasIndex(e => e.ControlNumber)
                .IsUnique();

            builder.HasIndex(wc => wc.ClosingDate);

            builder.Property(wc => wc.Status)
                   .HasConversion<string>()
                   .HasMaxLength(20);
        }
    }
}
