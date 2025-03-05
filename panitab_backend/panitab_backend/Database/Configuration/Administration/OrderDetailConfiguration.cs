using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using panitab_backend.Database.Entities.Administration;

namespace panitab_backend.Database.Configuration.Administration
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetailEntity>
    {
        public void Configure(EntityTypeBuilder<OrderDetailEntity> builder)
        {
            builder.Property(e => e.UnitPrice).HasPrecision(10, 2);
        }
    }
}
