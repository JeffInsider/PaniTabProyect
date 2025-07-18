using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using panitab_backend.Database.Entities.Administration;

namespace panitab_backend.Database.Configuration.Administration
{
    public class SupplierConfiguration : IEntityTypeConfiguration<SupplierEntity>
    {
        public void Configure(EntityTypeBuilder<SupplierEntity> builder)
        {
            builder.Property(e => e.Balance).HasPrecision(18, 2);
            builder.HasIndex(s => s.Name);
            builder.HasIndex(s => s.Phone);
        }
    }
}
