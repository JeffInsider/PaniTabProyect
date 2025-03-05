using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace panitab_backend.Database.Entities.Packer
{
    [Table("packer_payment", Schema = "dbo")]
    public class PackerPaymentEntity : BaseEntity
    {
        [Column("packer_id")]
        [Required]
        public Guid PackerId { get; set; }

        [ForeignKey("PackerId")]
        public virtual PackerEntity Packer { get; set; }

        [Column("payment_date")]
        [Required]
        public DateTime PaymentDate { get; set; } // Fecha en que se realizó el pago.

        [Column("total_bags_packed")]
        [Required]
        public int TotalBagsPacked { get; set; } // Total de bolsas empacadas en el período.

        [Column("amount_paid")]
        [Required]
        public decimal AmountPaid { get; set; } // Monto pagado.

        [Column("payment_completed")]
        [StringLength(20)]
        public bool PaymentCompleted { get; set; } = false; // Estado del pago (Ej: "Pagado", "Pendiente")

        [Column("last_packing_paid")]
        [StringLength(20)]
        public string LastPackingPaid { get; set; }
    }
}
