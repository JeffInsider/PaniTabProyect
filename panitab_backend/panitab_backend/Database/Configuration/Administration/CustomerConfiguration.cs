using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using panitab_backend.Database.Entities.Administration;

namespace panitab_backend.Database.Configuration.Administration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<CustomerEntity>
    {
        public void Configure(EntityTypeBuilder<CustomerEntity> builder)
        {
            builder.Property(e => e.Balance).HasPrecision(18, 2);

            // Opcional: añade índices para mejorar consultas
            builder.HasIndex(e => e.IdentityNumber).IsUnique();
            builder.HasIndex(e => e.PhoneNumber);
            builder.HasIndex(e => e.IsActive);

            //builder.HasQueryFilter(c => c.IsActive); // Filtro global para solo incluir clientes activos
        }
    }
}
