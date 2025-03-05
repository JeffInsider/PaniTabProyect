using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace panitab_backend.Database.Entities.Production
{
    [Table("material", Schema = "dbo")]
    public class MaterialEntity : BaseEntity
    {
        [StringLength(100)]
        [Column("name")]
        [Required]
        public string Name { get; set; }

        [StringLength(50)]
        [Column("category")]
        [Required]
        public string Category { get; set; }

        [Column("current_stock")]
        [Required]
        public decimal CurrentStock { get; set; }

        [Column("measure_unit")]
        [StringLength(50)]
        public string MeasureUnit { get; set; } // Unidad de medida (Ej: "kg", "litro", "unidad", "caja", etc.)

        [Column("is_active")]
        [Required]
        public bool IsActive { get; set; } = true;

        // Relación de "uno a muchos" con MaterialPurchase
        public virtual ICollection<MaterialPurchaseEntity> MaterialPurchases { get; set; }

        // Relación de "uno a muchos" con BreadClassMaterials
        public virtual ICollection<BreadClassMaterialEntity> BreadClassMaterials { get; set; }
    }
}
