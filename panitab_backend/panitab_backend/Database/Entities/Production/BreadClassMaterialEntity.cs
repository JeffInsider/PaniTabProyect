using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace panitab_backend.Database.Entities.Production
{
    [Table("bread_class_materials", Schema = "dbo")]
    public class BreadClassMaterialEntity : BaseEntity
    {
        [Column("bread_class_id")]
        [Required]
        public Guid BreadClassId { get; set; }

        [ForeignKey("BreadClassId")]
        public virtual BreadClassEntity BreadClass { get; set; }

        [Column("material_id")]
        [Required]
        public Guid MaterialId { get; set; }

        [ForeignKey("MaterialId")]
        public virtual MaterialEntity Material { get; set; }

        [Column("quantity_used")]
        [Required]
        public decimal QuantityUsed { get; set; } // Cantidad utilizada en la producción

        [Column("measure_unit")]
        [StringLength(50)]
        public string MeasureUnit { get; set; } // Unidad de medida
    }
}
