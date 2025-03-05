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

        [Column("is_completed")]
        [Required]
        public bool IsCompleted { get; set; } = false; //estado de el empaque

        public virtual ICollection<PackingDetailEntity> PackingDetails { get; set; }
        public virtual UserEntity CreatedByUser { get; set; }
        public virtual UserEntity UpdatedByUser { get; set; }
    }
}
