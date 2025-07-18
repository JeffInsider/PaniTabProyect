using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using panitab_backend.Database.Entities.Packer;

namespace panitab_backend.Database.Configuration.Packer
{
    public class PackingDetailConfiguration : IEntityTypeConfiguration<PackingDetailEntity>
    {
        public void Configure(EntityTypeBuilder<PackingDetailEntity> builder)
        {
            builder.HasIndex(pd => new { pd.PackingId, pd.BreadClassId });
        }
    }
}
