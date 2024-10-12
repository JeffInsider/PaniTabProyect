using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using panitab_backend.Database.Entities;

namespace panitab_backend.Database.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            
            // Configuración de las propiedades de UserEntity
            builder.Property(u => u.CreatedDate)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("first_name");

            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("last_name");

            builder.Property(u => u.UserName)
                .IsRequired();

            builder.Property(u => u.Email)
                .IsRequired();

            builder.Property(u => u.IsInactive)
                .IsRequired()
                .HasDefaultValue(false)
                .HasColumnName("is_inactive");

            builder.Property(u => u.PasswordResetToken)
                .HasMaxLength(300)
                .HasColumnName("password_reset_token");

            builder.Property(u => u.PasswordResetTokenExpires)
                .HasColumnName("password_reset_token_expires")
                .HasColumnType("datetime");
        }
    }
}
