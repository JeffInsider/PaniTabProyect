using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using panitab_backend.Database.Entities.Production;

namespace panitab_backend.Database.Entities.Administration
{
    [Table("supplier", Schema = "dbo")]
    public class SupplierEntity : BaseEntity
    {
        [StringLength(100)]
        [Column("name")]
        [Required]
        public string Name { get; set; }

        [Column("contact")]
        [StringLength(100)]
        public string Contact { get; set; }

        [Column("phone")]
        [StringLength(50)]
        public string Phone { get; set; }

        [Column("balance")]
        public decimal Balance { get; set; } // Saldo actual con el proveedor

        // Relación con las compras de material
        public virtual ICollection<MaterialPurchaseEntity> MaterialPurchases { get; set; }

        public virtual ICollection<SupplierPaymentEntity> SupplierPayments { get; set; }

    }
}
