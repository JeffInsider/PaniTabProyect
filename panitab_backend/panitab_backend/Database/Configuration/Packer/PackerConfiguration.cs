using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using panitab_backend.Database.Entities.Packer;

namespace panitab_backend.Database.Configuration.Packer
{
    public class PackerConfiguration : IEntityTypeConfiguration<PackerEntity>
    {
        public void Configure(EntityTypeBuilder<PackerEntity> builder)
        {
            builder.HasIndex(p => p.IdentityNumber).IsUnique();
            builder.HasIndex(p => p.IsActive);

            //builder.HasQueryFilter(p => p.IsActive); // Para soft delete
        }
    }
}
