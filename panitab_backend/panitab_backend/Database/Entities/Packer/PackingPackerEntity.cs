using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace panitab_backend.Database.Entities.Packer
{
    [Table("packing_packer", Schema = "dbo")]
    public class PackingPackerEntity : BaseEntity
    {
        [Column("packing_id")]
        [Required]
        public Guid PackingId { get; set; }

        [ForeignKey("PackingId")]
        public virtual PackingEntity Packing { get; set; }

        [Column("packer_id")]
        [Required]
        public Guid PackerId { get; set; }

        [ForeignKey("PackerId")]
        public virtual PackerEntity Packer { get; set; }
    }
}
