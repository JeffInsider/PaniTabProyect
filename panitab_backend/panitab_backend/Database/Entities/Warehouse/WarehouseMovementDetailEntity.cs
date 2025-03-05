using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using panitab_backend.Database.Entities.Production;

namespace panitab_backend.Database.Entities.Warehouse
{
    [Table("warehouse_movement_detail", Schema = "dbo")]
    public class WarehouseMovementDetailEntity : BaseEntity
    {
        [Column("warehouse_movement_id")]
        [Required]
        public Guid WarehouseMovementId { get; set; }

        [ForeignKey("WarehouseMovementId")]
        public virtual WarehouseMovementEntity WarehouseMovement { get; set; }

        [Column("warehouse_control_detail_id")]
        [Required]
        public Guid WarehouseControlDetailId { get; set; }

        [ForeignKey("WarehouseControlDetailId")]
        public virtual WarehouseControlDetailEntity WarehouseControlDetail { get; set; }

        [Column("bread_class_id")]
        [Required]
        public Guid BreadClassId { get; set; }

        [ForeignKey("BreadClassId")]
        public virtual BreadClassEntity BreadClass { get; set; }

        [Column("quantity")]
        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column("movement_type")]
        [StringLength(50)]
        public string MovementType { get; set; } //entrada o salida

        [Column("observations")]
        [StringLength(255)]
        public string Observations { get; set; }
    }
}