using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using panitab_backend.Database.Entities.Packer;

namespace panitab_backend.Database.Configuration.Packer
{
    public class PackingConfiguration : IEntityTypeConfiguration<PackingEntity>
    {
        public void Configure(EntityTypeBuilder<PackingEntity> builder)
        {
            builder.HasIndex(p => p.PackingNumer).IsUnique();
            builder.HasIndex(p => p.PackingDate);

            builder.Property(p => p.Status)
                   .HasConversion<string>()
                   .HasMaxLength(20);
        }
    }
}
