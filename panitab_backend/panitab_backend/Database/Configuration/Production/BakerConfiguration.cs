using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using panitab_backend.Database.Entities.Production;

namespace panitab_backend.Database.Configuration.Production
{
    public class BakerConfiguration : IEntityTypeConfiguration<BakerEntity>
    {
        public void Configure(EntityTypeBuilder<BakerEntity> builder)
        {
            builder.Property(e => e.Balance).HasPrecision(18, 2);

            builder.HasIndex(b => b.IdentityNumber).IsUnique();
            builder.HasIndex(b => b.IsActive);

            //builder.HasQueryFilter(b => b.IsActive);
        }
    }
}
