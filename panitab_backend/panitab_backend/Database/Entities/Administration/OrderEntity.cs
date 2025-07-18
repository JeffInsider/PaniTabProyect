using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace panitab_backend.Database.Entities.Administration
{
    [Table("order", Schema = "dbo")]
    public class OrderEntity : BaseEntity
    {
        [StringLength(50)]
        [Column("order_number")]
        [Required]
        public string OrderNumber { get; set; }

        [Column("order_date")]
        [Required]
        public DateTime OrderDate { get; set; }

        [Column("customer_id")]
        [Required]
        public Guid CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual CustomerEntity Customer { get; set; }

        [Column("assistant_id")]
        public Guid? AssistantId { get; set; }

        [ForeignKey("AssistantId")]
        public virtual CustomerAssistantEntity Assistant { get; set; }

        [Required]
        [Column("total_amount")]
        public decimal TotalAmount { get; set; }  //Se guarda el total al momento de crear la orden

        [Required]
        [Column("outstanding_balance")]
        public decimal OutstandingBalance { get; set; } // Lo que falta por pagar

        [Required]
        [Column("order_type")]
        [StringLength(20)]
        public string OrderType { get; set; } //"Credito", "Contado", "Empleado", "Particular"

        [Column("is_paid")]
        [Required]
        public bool IsPaid { get; set; } = false;

        [Column("parent_order_id")]
        public Guid? ParentOrderId { get; set; } // Si es una recarga, apunta a la orden original

        [ForeignKey("ParentOrderId")]
        public virtual OrderEntity ParentOrder { get; set; } // La orden original si es una recarga

        public virtual ICollection<OrderDetailEntity> OrderDetails { get; set; }
        public virtual ICollection<OrderEntity> refills { get; set; } = new List<OrderEntity>(); //recargas
    }
}
