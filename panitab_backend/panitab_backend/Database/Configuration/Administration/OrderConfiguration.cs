using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using panitab_backend.Database.Entities.Administration;

namespace panitab_backend.Database.Configuration.Administration
{
    public class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>
    {
        public void Configure(EntityTypeBuilder<OrderEntity> builder)
        {

            builder.Property(e => e.TotalAmount).HasPrecision(18, 2);
            builder.Property(e => e.OutstandingBalance).HasPrecision(18, 2);

            // Índices
            builder.HasIndex(o => o.OrderNumber).IsUnique();
            builder.HasIndex(o => new { o.CustomerId, o.OrderDate });
            builder.HasIndex(o => o.IsPaid);

            // Relaciones
            builder.HasOne(o => o.Customer)
                   .WithMany(c => c.Orders)
                   .OnDelete(DeleteBehavior.Restrict);

            // Configuración de enum
            builder.Property(o => o.OrderType)
                   .HasConversion<string>()
                   .HasMaxLength(20);
        }
    }
}
