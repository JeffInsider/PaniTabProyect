using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace panitab_backend.Database.Entities.Production
{
    [Table("baker", Schema = "dbo")]
    public class BakerEntity : BaseEntity
    {
        [StringLength(20, MinimumLength = 10)]
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

        [Column("balance")]
        public decimal Balance { get; set; } // Se acumulan sobrantes o faltantes

        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        public virtual ICollection<ProductionDetailEntity> ProductionDetails { get; set; }

        public virtual ICollection<BakerPaymentEntity> BakerPayments { get; set; } // Relación con pagos del panadero
    }
}
