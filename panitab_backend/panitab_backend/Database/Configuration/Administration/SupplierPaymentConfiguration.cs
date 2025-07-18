using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using panitab_backend.Database.Entities.Administration;

namespace panitab_backend.Database.Configuration.Administration
{
    public class SupplierPaymentConfiguration : IEntityTypeConfiguration<SupplierPaymentEntity>
    {
        public void Configure(EntityTypeBuilder<SupplierPaymentEntity> builder)
        {
            builder.Property(e => e.AmountPaid).HasPrecision(18, 2);
            builder.Property(e => e.BalanceRemaining).HasPrecision(18, 2);

            builder.HasIndex(sp => sp.PaymentDate);
            builder.HasIndex(sp => sp.SupplierId);

            builder.Property(sp => sp.PaymentMethod)
                   .HasConversion<string>()
                   .HasMaxLength(50);
        }
    }
}
