using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace panitab_backend.Database.Entities.Warehouse
{
    [Table("warehouse_control", Schema = "dbo")]
    public class WarehouseControlEntity : BaseEntity
    {
        [StringLength(50)]
        [Column("control_number")]
        [Required]
        public string ControlNumber { get; set; }

        [Column("control_date")]
        [Required]
        public DateTime ClosingDate { get; set; }

        [Column("observations")]
        [StringLength(255)]
        public string Observations { get; set; }

        [Column("is_completed")]
        [Required]
        public bool IsCompleted { get; set; } = false; //estado de el control de inventario (Ej: "En proceso", "Finalizado")

        [Column("last_closing_date")]
        public DateTime? LastClosingDate { get; set; }

        public virtual ICollection<WarehouseControlDetailEntity> WarehouseControlDetails { get; set; }
        public virtual UserEntity CreatedByUser { get; set; }
        public virtual UserEntity UpdatedByUser { get; set; }
    }
}
