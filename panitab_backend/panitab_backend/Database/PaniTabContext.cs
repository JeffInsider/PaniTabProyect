using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using panitab_backend.Database.Configuration;
using panitab_backend.Database.Entities;

namespace panitab_backend.Database
{
    public class PaniTabContext : IdentityDbContext<UserEntity, IdentityRole, string>
    {
        public PaniTabContext(DbContextOptions<PaniTabContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("Security");

            builder.Entity<UserEntity>().ToTable("users");
            builder.Entity<IdentityRole>().ToTable("roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("user_roles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("user_claims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("user_logins");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("role_claims");
            builder.Entity<IdentityUserToken<string>>().ToTable("user_tokens");

            // Configuración de las propiedades de UserEntity
            builder.ApplyConfiguration(new UserConfiguration());

            // restriccion de eliminacion en cascada
            var entityTypes = builder.Model.GetEntityTypes();
            foreach (var entityType in entityTypes)
            {
                var foreignKeys = entityType.GetForeignKeys();
                foreach (var foreignKey in foreignKeys)
                {
                    foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
                }
            }
        }

        public DbSet<UserEntity> Users { get; set; }
    }
}
