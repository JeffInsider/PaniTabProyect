using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace panitab_backend.Database.Entities.Production
{
    [Table("production", Schema = "dbo")]
    public class ProductionEntity : BaseEntity
    {
        [StringLength(70, MinimumLength = 3)]
        [Column("production_number")]
        [Required]
        public string ProductionNumber { get; set; }

        [Column("production_date")]
        [Required]
        public DateTime ProductionDate { get; set; }

        [Column("is_completed")]
        public bool IsCompleted { get; set; } = false; // Estado (Ej: "En proceso", "Finalizado")

        public virtual ICollection<ProductionDetailEntity> ProductionDetails { get; set; }
        public virtual UserEntity CreatedByUser { get; set; }
        public virtual UserEntity UpdatedByUser { get; set; }
    }
}