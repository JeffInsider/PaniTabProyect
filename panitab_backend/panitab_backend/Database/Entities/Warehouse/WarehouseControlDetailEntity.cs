using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using panitab_backend.Database.Entities.Administration;
using panitab_backend.Database.Entities.Packer;
using panitab_backend.Database.Entities.Production;

namespace panitab_backend.Database.Entities.Warehouse
{
    [Table("warehouse_control_detail", Schema = "dbo")]
    public class WarehouseControlDetailEntity : BaseEntity
    {
        [Column("warehouse_control_id")]
        [Required]
        public Guid WarehouseControlId { get; set; }

        [ForeignKey("WarehouseControlId")]
        public virtual WarehouseControlEntity WarehouseControl { get; set; }

        [Column("bread_class_id")]
        [Required]
        public Guid BreadClassId { get; set; }

        [ForeignKey("BreadClassId")]
        public virtual BreadClassEntity BreadClass { get; set; }

        [Column("initial_stock")]
        [Required]
        public int InitialStock { get; set; }

        //mostrar el total de pan que se recibe en la bodega
        [Column("incoming_stock")]
        [Required]
        public int IncomingStock { get; set; }

        // Referencia a PackingEntity para las ENTRADAS de pan a bodega y poder ver separados los empaques
        [Column("packing_id")]
        public Guid? PackingId { get; set; }

        [ForeignKey("PackingId")]
        public virtual PackingEntity Packing { get; set; }

        [Column("order_id")]
        public Guid? OrderId { get; set; }

        [ForeignKey("OrderId")]
        public virtual OrderEntity Order { get; set; }

        [StringLength(50)]
        [Column("order_type")]
        [Required]
        public string OrderType { get; set; } //pedido o recarga

        [Column("adjustments")]
        public int Adjustments { get; set; } // cambios en el stock

        [Column("outgoing_stock")]
        [Required]
        public int OutgoingStock { get; set; } // Pan que se saca de la bodega

        [Column("damaged_stock")]
        [Required]
        public int DamagedStock { get; set; } // pan dañado

        [Column("final_stock")]
        [Required]
        public int FinalStock { get; set; } // Stock teórico

        [Column("real_stock")]
        [Required]
        public int RealStock { get; set; } // Existencia real después del conteo

        [Column("difference")]
        [Required]
        public int Difference { get; set; } // Diferencia entre RealStock y FinalStock

        // Nuevas propiedades para ajustarlo
        [Column("shortage")]
        public int? Shortage { get; set; } // Faltante

        [Column("excess")]
        public int? Excess { get; set; } // Sobrante
        public virtual ICollection<WarehouseMovementDetailEntity> WarehouseMovementDetails { get; set; }
    }
}