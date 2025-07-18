using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using panitab_backend.Database.Entities.Production;

namespace panitab_backend.Database.Entities.Administration
{
    [Table("order_detail", Schema = "dbo")]
    public class OrderDetailEntity : BaseEntity
    {
        [Column("order_id")]
        [Required]
        public Guid OrderId { get; set; }

        [ForeignKey("OrderId")]
        public virtual OrderEntity Order { get; set; }

        [Column("bread_class_id")]
        [Required]
        public Guid BreadClassId { get; set; }

        [ForeignKey("BreadClassId")]
        public virtual BreadClassEntity BreadClass { get; set; }

        [Column("quantity")]
        [Required]
        public int Quantity { get; set; }

        //decimal 18,2
        [Column("unit_price")]
        [Required]
        public decimal UnitPrice { get; set; }

    }
}
