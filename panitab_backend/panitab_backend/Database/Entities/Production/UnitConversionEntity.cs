using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace panitab_backend.Database.Entities.Production
{
    [Table("unit_conversion", Schema = "dbo")]
    public class UnitConversionEntity : BaseEntity
    {
        [Column("material_id")]
        [Required]
        public Guid MaterialId { get; set; }

        [ForeignKey("MaterialId")]
        public virtual MaterialEntity Material { get; set; }

        [Column("from_unit")]
        [Required]
        [StringLength(50)]
        public string FromUnit { get; set; } // Ejemplo: "Quintal", "Kg", "Paquete"

        [Column("to_unit")]
        [Required]
        [StringLength(50)]
        public string ToUnit { get; set; } // La unidad base ("Lb" o "Libras")

        [Column("conversion_factor")]
        [Required]
        public decimal ConversionFactor { get; set; } // Factor de conversión a la unidad base
    }
}
