using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using panitab_backend.Database.Entities.Production;

namespace panitab_backend.Database.Entities.Packer
{
    [Table("packing_detail", Schema = "dbo")]
    public class PackingDetailEntity : BaseEntity
    {
        [Column("packing_id")]
        [Required]
        public Guid PackingId { get; set; }

        [ForeignKey("PackingId")]
        public virtual PackingEntity Packing { get; set; }

        //se liga a una produccion
        [Column("production_id")]
        [Required]
        public Guid ProductionId { get; set; }

        [ForeignKey("ProductionId")]
        public virtual ProductionEntity Production { get; set; }

        //se liga a una clase de pan
        [Column("bread_class_id")]
        [Required]
        public Guid BreadClassId { get; set; }

        [ForeignKey("BreadClassId")]
        public virtual BreadClassEntity BreadClass { get; set; }

        //que empacador lo empaco
        //[Column("packer_id")]
        //[Required]
        //public Guid PackerId { get; set; }

        //[ForeignKey("PackerId")]
        //public virtual PackerEntity Packer { get; set; }


        [Column("total_packed")]
        [Required]
        public int TotalPacked { get; set; }

        [Column("damaged_in_packing")]
        [Required]
        public int DamagedInPacking { get; set; }

        [Column("description_damaged")]
        [StringLength(255)]
        public string DescriptionDamaged { get; set; }

        [Column("difference")]
        //diferencia por cada clase de pan
        public int Difference { get; set; }

        //relacion con los empacadores atravez de la tabla packing_packer
        public virtual ICollection<PackingPackerEntity> PackingPackers { get; set; }
    }
}