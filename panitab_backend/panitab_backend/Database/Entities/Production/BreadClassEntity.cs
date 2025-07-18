using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using panitab_backend.Database.Entities.Administration;
using panitab_backend.Database.Entities.Warehouse;

namespace panitab_backend.Database.Entities.Production
{
    [Table("bread_class", Schema = "dbo")]
    public class BreadClassEntity : BaseEntity
    {
        [StringLength(100)]
        [Column("name")]
        [Required]
        public string Name { get; set; }

        [StringLength(50)]
        [Column("category")]
        [Required]
        public string Category { get; set; }

        [Column("trays_per_quintal")]
        [Required]
        public decimal TraysPerQuintal { get; set; } // latas por una arroba

        [Column("breads_per_tray")]
        [Required]
        public decimal BreadsPerTray { get; set; } // panes por lata

        [Column ("breads_per_bag")]
        [Required]
        public int BreadsPerBag { get; set; } // panes por bolsa

        [Column("price_per_quintal")]
        [Required]
        public decimal PricePerQuintal { get; set; } // precio por arroba

        [Column("packaging_price")]
        [Required]
        public decimal PackagingPrice { get; set; } // precio de empaque

        [Column("customer_price")]
        [Required]
        public decimal CustomerPrice { get; set; } // precio de vendedor

        [Column("public_price")]
        [Required]
        public decimal PublicPrice { get; set; } // precio al publico

        [Column("is_active")]
        [Required]
        public bool IsActive { get; set; } = true; // activo

        // Relación de "uno a muchos" con BreadClassMaterials
        public virtual ICollection<BreadClassMaterialEntity> BreadClassMaterials { get; set; }

        // Relación de "uno a muchos" con OrderDetail
        public virtual ICollection<OrderDetailEntity> OrderDetails { get; set; }

    }
}
