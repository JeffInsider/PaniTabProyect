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

        public enum ProductionStatus
        {
            Pending,
            InProgress,
            Completed,
            Cancelled
        }
        [Column("status")]
        [Required]
        public ProductionStatus Status { get; set; } = ProductionStatus.Pending;

        public virtual ICollection<ProductionDetailEntity> ProductionDetails { get; set; }
    }
}