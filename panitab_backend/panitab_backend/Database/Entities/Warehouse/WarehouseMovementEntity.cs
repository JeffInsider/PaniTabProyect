using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace panitab_backend.Database.Entities.Warehouse
{
    [Table("warehouse_movement", Schema = "dbo")]
    public class WarehouseMovementEntity : BaseEntity
    {
        [Required]
        [Column("movement_number")]
        [StringLength(50)]
        public string MovementNumber { get; set; }

        [Required]
        public DateTime MovementDate { get; set; }

        public virtual ICollection<WarehouseMovementDetailEntity> WarehouseMovementDetails { get; set; }
        public virtual UserEntity CreatedByUser { get; set; }
        public virtual UserEntity UpdatedByUser { get; set; }
    }
}
