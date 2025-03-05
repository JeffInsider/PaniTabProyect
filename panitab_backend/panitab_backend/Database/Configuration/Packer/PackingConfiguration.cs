using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using panitab_backend.Database.Entities.Packer;

namespace panitab_backend.Database.Configuration.Packer
{
    public class PackingConfiguration : IEntityTypeConfiguration<PackingEntity>
    {
        public void Configure(EntityTypeBuilder<PackingEntity> builder)
        {
            builder.HasOne(e => e.CreatedByUser)
                .WithMany()
                .HasForeignKey(e => e.CreatedBy)
                .HasPrincipalKey(e => e.Id)
                .IsRequired();

            builder.HasOne(e => e.UpdatedByUser)
                .WithMany()
                .HasForeignKey(e => e.UpdatedBy)
                .HasPrincipalKey(e => e.Id)
                .IsRequired();
        }
    }
}
