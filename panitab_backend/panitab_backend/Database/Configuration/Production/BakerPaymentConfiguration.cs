using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using panitab_backend.Database.Entities.Production;

namespace panitab_backend.Database.Configuration.Production
{
    public class BakerPaymentConfiguration : IEntityTypeConfiguration<BakerPaymentEntity>
    {
        public void Configure(EntityTypeBuilder<BakerPaymentEntity> builder)
        {
            builder.Property(e => e.TotalOfQuintal).HasPrecision(10, 2);
            builder.Property(e => e.AmountPaid).HasPrecision(10, 2);
            builder.Property(e => e.BalanceAdjustment).HasPrecision(10, 2);
        }
    }
}
