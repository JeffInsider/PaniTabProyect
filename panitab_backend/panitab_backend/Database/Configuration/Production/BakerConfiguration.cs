using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using panitab_backend.Database.Entities.Production;

namespace panitab_backend.Database.Configuration.Production
{
    public class BakerConfiguration : IEntityTypeConfiguration<BakerEntity>
    {
        public void Configure(EntityTypeBuilder<BakerEntity> builder)
        {
            builder.Property(e => e.Balance).HasPrecision(10, 2);
        }
    }
}
