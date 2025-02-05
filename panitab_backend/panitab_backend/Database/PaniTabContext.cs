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

    //public class PaniTabContext : IdentityDbContext<UserEntity, IdentityRole, string>
    //{
    //    public PaniTabContext(DbContextOptions<PaniTabContext> options) : base(options)
    //    {
    //    }

    //    protected override void OnModelCreating(ModelBuilder builder)
    //    {
    //        base.OnModelCreating(builder);
    //        builder.HasDefaultSchema("Security");

    //        builder.Entity<UserEntity>().ToTable("users");
    //        builder.Entity<IdentityRole>().ToTable("roles");
    //        builder.Entity<IdentityUserRole<string>>().ToTable("user_roles");
    //        builder.Entity<IdentityUserClaim<string>>().ToTable("user_claims");
    //        builder.Entity<IdentityUserLogin<string>>().ToTable("user_logins");
    //        builder.Entity<IdentityRoleClaim<string>>().ToTable("role_claims");
    //        builder.Entity<IdentityUserToken<string>>().ToTable("user_tokens");

    //        // Configuración de las propiedades de UserEntity esto va a la clase UserConfiguration
    //        builder.ApplyConfiguration(new UserConfiguration());

    //        // restriccion de eliminacion en cascada
    //        var entityTypes = builder.Model.GetEntityTypes();
    //        foreach (var entityType in entityTypes)
    //        {
    //            var foreignKeys = entityType.GetForeignKeys();
    //            foreach (var foreignKey in foreignKeys)
    //            {
    //                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
    //            }
    //        }
    //    }

    //    public DbSet<UserEntity> Users { get; set; }
    //}
}
