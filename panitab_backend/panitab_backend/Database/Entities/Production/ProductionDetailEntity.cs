using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace panitab_backend.Database.Entities.Production
{
    [Table("production_detail", Schema = "dbo")]
    public class ProductionDetailEntity : BaseEntity
    {
        [StringLength(50)]
        [Column("production_id")]
        [Required]
        public Guid ProductionId { get; set; }

        [ForeignKey("ProductionId")]
        public virtual ProductionEntity Production { get; set; }

        [StringLength(50)]
        [Column("baker_id")]
        [Required]
        public Guid BakerId { get; set; } // Relaciona con el panadero

        [ForeignKey("BakerId")]
        public virtual BakerEntity Baker { get; set; } // Relación con el panadero

        [StringLength(50)]
        [Column("bread_class_id")]
        [Required]
        public Guid BreadClassId { get; set; }

        [ForeignKey("BreadClassId")]
        public virtual BreadClassEntity BreadClass { get; set; }

        [Column("quantity_arrobas")]
        [Required]
        public int QuantityQuintal { get; set; } // Quintales producidos

        [Column("quantity_latas")]
        [Required]
        public int QuantityLatas { get; set; } // Latas producidas

        [Column("difference")]
        public int Difference { get; set; } // Diferencia en latas

        public enum ProductionStatus
        {
            Pending,
            Completed,
            Cancelled
        }
        [Column("production_status")]
        [Required]
        public ProductionStatus Status { get; set; } = ProductionStatus.Pending; // Estado de la producción
    }
}