using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using panitab_backend.Database.Entities.Production;

namespace panitab_backend.Database.Configuration.Production
{
    public class BakerPaymentConfiguration : IEntityTypeConfiguration<BakerPaymentEntity>
    {
        public void Configure(EntityTypeBuilder<BakerPaymentEntity> builder)
        {
            builder.Property(e => e.TotalOfQuintal).HasPrecision(18, 2);
            builder.Property(e => e.AmountPaid).HasPrecision(18, 2);
            builder.Property(e => e.BalanceAdjustment).HasPrecision(18, 2);

            builder.HasIndex(bp => bp.PaymentDate);
            builder.HasIndex(bp => bp.BakerId);

            builder.Property(bp => bp.PaymentStatus)
                   .HasConversion<string>()
                   .HasMaxLength(20);
        }
    }
}
