using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using panitab_backend.Database.Configuration;
using panitab_backend.Database.Configuration.Administration;
using panitab_backend.Database.Configuration.Packer;
using panitab_backend.Database.Configuration.Production;
using panitab_backend.Database.Configuration.Warehouse;
using panitab_backend.Database.Entities;
using panitab_backend.Database.Entities.Administration;
using panitab_backend.Database.Entities.Packer;
using panitab_backend.Database.Entities.Production;
using panitab_backend.Database.Entities.Warehouse;
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

            //configuracion de las entidades
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
            modelBuilder.ApplyConfiguration(new SupplierConfiguration());
            modelBuilder.ApplyConfiguration(new SupplierPaymentConfiguration());
            modelBuilder.ApplyConfiguration(new PackerConfiguration());
            modelBuilder.ApplyConfiguration(new PackerPaymentConfiguration());
            modelBuilder.ApplyConfiguration(new PackingConfiguration());
            modelBuilder.ApplyConfiguration(new PackingDetailConfiguration());
            modelBuilder.ApplyConfiguration(new PackingPackerConfiguration());
            modelBuilder.ApplyConfiguration(new BakerConfiguration());
            modelBuilder.ApplyConfiguration(new BakerPaymentConfiguration());
            modelBuilder.ApplyConfiguration(new BreadClassConfiguration());
            modelBuilder.ApplyConfiguration(new BreadClassMaterialConfiguration());
            modelBuilder.ApplyConfiguration(new MaterialConfiguration());
            modelBuilder.ApplyConfiguration(new MaterialPurchaseConfiguration());
            modelBuilder.ApplyConfiguration(new ProductionConfiguration());
            modelBuilder.ApplyConfiguration(new ProductionDetailConfiguration());
            modelBuilder.ApplyConfiguration(new UnitConversionConfiguration());
            modelBuilder.ApplyConfiguration(new WarehouseControlConfiguration());
            modelBuilder.ApplyConfiguration(new WarehouseControlDetailConfiguration());
            modelBuilder.ApplyConfiguration(new WarehouseMovementConfiguration());
            modelBuilder.ApplyConfiguration(new WarehouseMovementDetailConfiguration());

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

        //sobreescribir el metodo saveChangeAsync del DbContext para hacer algo antes de guardar los cambios de forma asincrona
        //public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //{
        //    var entries = ChangeTracker.Entries().Where(e => e.Entity is BaseEntity && (
        //        e.State == EntityState.Added || 
        //        e.State == EntityState.Modified
        //        ));

        //    foreach (var entry in entries)
        //    {
        //        var entity = entry.Entity as BaseEntity;

        //        if (entity != null)
        //        {
        //            if (entry.State == EntityState.Added)
        //            {
        //                entity.CreatedBy = _auditService.GetUserId() ?? "system";
        //                entity.CreatedDate = DateTime.UtcNow;
        //            }
        //            else
        //            {
        //                entity.UpdatedBy = _auditService.GetUserId();
        //                entity.UpdatedDate = DateTime.UtcNow;
        //            }
        //        }
        //    }
        //    return base.SaveChangesAsync(cancellationToken);
        //}
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseEntity && (
                    e.State == EntityState.Added ||
                    e.State == EntityState.Modified));

            var userId = _auditService.GetUserId() ?? "system"; // Asegura un valor por defecto

            foreach (var entry in entries)
            {
                var entity = (BaseEntity)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedBy = userId;
                    entity.CreatedDate = DateTime.UtcNow;
                }
                else
                {
                    entry.Property("CreatedBy").IsModified = false;
                    entry.Property("CreatedDate").IsModified = false;

                    entity.UpdatedBy = userId;
                    entity.UpdatedDate = DateTime.UtcNow;
                }
            }

            try
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException ex)
            {
                var sqlException = ex.InnerException as SqlException;
                // Loggear el error SQL específico
                throw new Exception($"Database error: {sqlException?.Message}");
            }
        }

        //Aqui iran los DbSet de las entidades
        public DbSet<CustomerAssistantEntity> CustomerAssistants { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<OrderDetailEntity> OrdersDetails { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<SupplierEntity> Suppliers { get; set; }
        public DbSet<SupplierPaymentEntity> SupplierPayments { get; set; }
        public DbSet<PackerEntity> Packers { get; set; }
        public DbSet<PackerPaymentEntity> PackerPayments { get; set; }
        public DbSet<PackingDetailEntity> PackingDetails { get; set; }
        public DbSet<PackingEntity> Packings { get; set; }
        public DbSet<BakerEntity> Bakers { get; set; }
        public DbSet<BakerPaymentEntity> BakerPayments { get; set; }
        public DbSet<BreadClassEntity> BreadClasses { get; set; }
        public DbSet<BreadClassMaterialEntity> BreadClassMaterials { get; set; }
        public DbSet<MaterialEntity> Materials { get; set; }
        public DbSet<MaterialPurchaseEntity> MaterialPurchases { get; set; }
        public DbSet<ProductionDetailEntity> ProductionDetails { get; set; }
        public DbSet<ProductionEntity> Productions { get; set; }
        public DbSet<UnitConversionEntity> UnitConversions { get; set; }
        public DbSet<WarehouseControlDetailEntity> WarehouseControlDetails { get; set; }
        public DbSet<WarehouseControlEntity> WarehouseControls { get; set; }
        public DbSet<WarehouseMovementDetailEntity> WarehouseMovementDetails { get; set; }
        public DbSet<WarehouseMovementEntity> WarehouseMovements { get; set; }

    }
}
