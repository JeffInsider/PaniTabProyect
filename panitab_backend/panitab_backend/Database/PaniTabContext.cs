using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using panitab_backend.Database.Configuration;
using panitab_backend.Database.Entities;
using panitab_backend.Services.Interfaces;

namespace panitab_backend.Database
{
    public class PaniTabContext : IdentityDbContext<UserEntity>
    {
        private readonly IAuditService _auditService;

        public PaniTabContext(
            DbContextOptions options, 
            IAuditService auditService
            ) : base(options)
        {//aqui se plantea toda la configuracion de la base de datos
            this._auditService = auditService;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //importante configurar en el sql server la collation de la base de datos
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");
            
            modelBuilder.HasDefaultSchema("Security");

            modelBuilder.Entity<UserEntity>().ToTable("users");
            modelBuilder.Entity<IdentityRole>().ToTable("roles");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("user_roles");

            //permisos
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("user_claims");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("roles_claims");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("users_logins");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("users_tokens");

            //restringir las keys para que no se eliminen en cascada
            var eTypes = modelBuilder.Model.GetEntityTypes(); // todo el listado de entidades
            foreach (var type in eTypes)
            {
                var foreignKeys = type.GetForeignKeys();
                foreach (var fk in foreignKeys)
                {
                    fk.DeleteBehavior = DeleteBehavior.Restrict;
                }
            }
        }

        //sobreescribir el metodo saveChangeAsync del DbContext para hacer algo antes de guardar los cambios
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries().Where(e => e.Entity is BaseEntity && (
                e.State == EntityState.Added || 
                e.State == EntityState.Modified
                ));

            foreach (var entry in entries)
            {
                var entity = entry.Entity as BaseEntity;

                if (entity != null)
                {
                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedBy = _auditService.GetUserId();
                        entity.CreatedDate = DateTime.UtcNow;
                    }
                    else
                    {
                        entity.UpdatedBy = _auditService.GetUserId();
                        entity.UpdatedDate = DateTime.UtcNow;
                    }
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }

    //Aqui iran los DbSet de las entidades
}
