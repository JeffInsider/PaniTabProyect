using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace panitab_backend.Database.Entities.Production
{
    [Table("baker_payment", Schema = "dbo")]
    public class BakerPaymentEntity : BaseEntity
    {
        [StringLength(50)]
        [Column("baker_id")]
        [Required]
        public Guid BakerId { get; set; }

        [ForeignKey("BakerId")]
        public virtual BakerEntity Baker { get; set; }

        [Column("payment_date")]
        public DateTime PaymentDate { get; set; } // Fecha del pago

        [Column("total_arrobas")]
        public decimal TotalOfQuintal { get; set; }

        [Column("amount_paid")]
        public decimal AmountPaid { get; set; } // Monto a pagar al panadero

        public enum BakerStatus
        {
            Pending,
            Completed,
            Cancelled
        }

        [Column("payment_status")]
        [Required]
        public BakerStatus PaymentStatus { get; set; } = BakerStatus.Pending; // Estado del pago

        [Column("balance_adjustment")]
        public decimal BalanceAdjustment { get; set; } // Ajuste de balance

        [Column("last_production_paid")]
        [StringLength(20)]
        public string LastProductionPaid { get; set; } // Última producción pagada
    }
}
