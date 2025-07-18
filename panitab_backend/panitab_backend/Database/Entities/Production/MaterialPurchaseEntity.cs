using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using panitab_backend.Database.Entities.Administration;

namespace panitab_backend.Database.Entities.Production
{
    [Table("material_purchase", Schema = "dbo")]
    public class MaterialPurchaseEntity : BaseEntity
    {
        [Column("material_id")]
        [Required]
        public Guid MaterialId { get; set; }

        [ForeignKey("MaterialId")]
        public virtual MaterialEntity Material { get; set; }

        [Column("supplier_id")]
        [Required]
        public Guid SupplierId { get; set; }

        [ForeignKey("SupplierId")]
        public virtual SupplierEntity Supplier { get; set; }

        [Column("purchase_date")]
        [Required]
        public DateTime PurchaseDate { get; set; }

        [Column("unit_price")]
        [Required]
        public decimal UnitPrice { get; set; } // Precio por unidad

        [Column("quantity_purchased")]
        [Required]
        public decimal QuantityPurchased { get; set; } // Cantidad comprada

        [Column("balance")]
        public decimal Balance { get; set; } // Saldo a pagar al proveedor

        public enum MaterialStatus
        {
            Pending,
            Completed,
            Cancelled
        }

        [Column("purchase_status")]
        [Required]
        public MaterialStatus PurchaseStatus { get; set; } = MaterialStatus.Pending; // Estado de la compra

        [Column("measure_unit")]
        [StringLength(50)]
        public string MeasureUnit { get; set; } // Unidad de medida (Ej: "Kg", "Lb", "Unidad", "paquete")
    }
}
