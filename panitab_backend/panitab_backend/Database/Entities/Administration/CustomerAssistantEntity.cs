using System.ComponentModel.DataAnnotations.Schema;

namespace panitab_backend.Database.Entities.Administration
{
    [Table("customer_assistant", Schema ="dbo")]
    public class CustomerAssistantEntity : BaseEntity
    {
        [Column("customer_id")]
        public Guid CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual CustomerEntity Customer { get; set; }

        [Column("assistant_name")]
        public string AssistantName { get; set; }

        //agregar is active
        [Column("is_active")]
        public bool IsActive { get; set; } = true;
    }
}
