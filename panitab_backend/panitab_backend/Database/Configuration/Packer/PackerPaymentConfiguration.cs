using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using panitab_backend.Database.Entities.Packer;

namespace panitab_backend.Database.Configuration.Packer
{
    public class PackerPaymentConfiguration : IEntityTypeConfiguration<PackerPaymentEntity>
    {
        public void Configure(EntityTypeBuilder<PackerPaymentEntity> builder)
        {
            builder.Property(e => e.AmountPaid).HasPrecision(18, 2);
            builder.HasIndex(pp => pp.PaymentDate);
            builder.HasIndex(pp => pp.PackerId);
        }
    }
}
