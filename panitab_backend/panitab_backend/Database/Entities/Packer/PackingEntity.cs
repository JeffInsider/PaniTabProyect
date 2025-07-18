using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace panitab_backend.Database.Entities.Packer
{
    [Table("packing", Schema = "dbo")]
    public class PackingEntity : BaseEntity
    {
        [StringLength(50)]
        [Column("packing_number")]
        [Required]
        public string PackingNumer { get; set; }

        [Column("packing_date")]
        [Required]
        public DateTime PackingDate { get; set; }

        public enum PackingStatus
        {
            Pending,
            Completed,
            Cancelled
        }

        [Column("status")]
        [Required]
        public PackingStatus Status { get; set; } = PackingStatus.Pending;

        public virtual ICollection<PackingDetailEntity> PackingDetails { get; set; }

        // En PackingEntity.cs
        public virtual ICollection<PackingPackerEntity> PackingPackers { get; set; }
    }
}
