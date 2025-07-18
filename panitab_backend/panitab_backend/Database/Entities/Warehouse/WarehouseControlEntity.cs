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

        public enum WarehouseStatus
        {
            Open,
            Closed,
            Cancelled
        }
        [Column("status")]
        [Required]
        public WarehouseStatus Status { get; set; } = WarehouseStatus.Open;

        [Column("last_closing_date")]
        public DateTime? LastClosingDate { get; set; }

        public virtual ICollection<WarehouseControlDetailEntity> WarehouseControlDetails { get; set; }
    }
}
