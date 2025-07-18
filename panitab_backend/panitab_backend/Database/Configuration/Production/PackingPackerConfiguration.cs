using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using panitab_backend.Database.Entities.Packer;

namespace panitab_backend.Database.Configuration.Production
{
    public class PackingPackerConfiguration : IEntityTypeConfiguration<PackingPackerEntity>
    {
        public void Configure(EntityTypeBuilder<PackingPackerEntity> builder)
        {
            // Llave compuesta
            builder.HasKey(pp => new { pp.PackingId, pp.PackerId });

            builder.HasOne(pp => pp.Packing)
                   .WithMany(p => p.PackingPackers)
                   .HasForeignKey(pp => pp.PackingId);

            builder.HasOne(pp => pp.Packer)
                   .WithMany(p => p.PackingPackers)
                   .HasForeignKey(pp => pp.PackerId);
        }
    }
}
