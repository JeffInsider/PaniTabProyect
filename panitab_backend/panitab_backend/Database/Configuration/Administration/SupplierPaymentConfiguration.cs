using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using panitab_backend.Database.Entities.Administration;

namespace panitab_backend.Database.Configuration.Administration
{
    public class SupplierPaymentConfiguration : IEntityTypeConfiguration<SupplierPaymentEntity>
    {
        public void Configure(EntityTypeBuilder<SupplierPaymentEntity> builder)
        {
            builder.Property(e => e.AmountPaid).HasPrecision(10, 2);
            builder.Property(e => e.BalanceRemaining).HasPrecision(10, 2);
        }
    }
}
