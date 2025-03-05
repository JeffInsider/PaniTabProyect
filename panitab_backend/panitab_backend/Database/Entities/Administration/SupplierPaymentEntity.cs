using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace panitab_backend.Database.Entities.Administration
{
    [Table("supplier_payment", Schema = "dbo")]
    public class SupplierPaymentEntity : BaseEntity
    {
        [Column("supplier_id")]
        [Required]
        public Guid SupplierId { get; set; }

        [ForeignKey("SupplierId")]
        public virtual SupplierEntity Supplier { get; set; } // Relación con el proveedor

        [Column("payment_date")]
        [Required]
        public DateTime PaymentDate { get; set; } // Fecha en que se realizó el pago

        [Column("amount_paid")]
        [Required]
        public decimal AmountPaid { get; set; } // Monto pagado

        [Column("payment_method")]
        [StringLength(50)]
        public string PaymentMethod { get; set; } // Método de pago (Ej. "Transferencia", "Cheque", "Efectivo")

        [Column("balance_remaining")]
        public decimal BalanceRemaining { get; set; } // Saldo restante después del pago
    }
}
