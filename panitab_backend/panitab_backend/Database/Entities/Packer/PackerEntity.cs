using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace panitab_backend.Database.Entities.Packer
{
    [Table("packer", Schema = "dbo")]
    public class PackerEntity : BaseEntity
    {
        [StringLength(50)]
        [Column("identity_number")]
        [Required]
        public string IdentityNumber { get; set; }

        [StringLength(100)]
        [Column("first_name")]
        [Required]
        public string FirstName { get; set; }

        [StringLength(100)]
        [Column("last_name")]
        [Required]
        public string LastName { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        public virtual ICollection<PackingEntity> PackingDetails { get; set; }

        // Agregar esta colección para los pagos del empacador
        public virtual ICollection<PackerPaymentEntity> PackerPayments { get; set; } // Relación con pagos del empacador

    }
}
