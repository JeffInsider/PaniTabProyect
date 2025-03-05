using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using panitab_backend.Database.Entities.Production;

namespace panitab_backend.Database.Configuration.Production
{
    public class ProductionConfiguration : IEntityTypeConfiguration<ProductionEntity>
    {
        public void Configure(EntityTypeBuilder<ProductionEntity> builder)
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
