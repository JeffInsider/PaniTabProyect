using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace panitab_backend.Database.Entities.Administration
{
    [Table("customer", Schema = "dbo")]
    public class CustomerEntity : BaseEntity
    {
        [StringLength(50)]
        [Column("identity_number")]
        [Required]
        public string IdentityNumber { get; set; }

        [StringLength(50)]
        [Column("first_name")]
        [Required]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Column("last_name")]
        [Required]
        public string LastName { get; set; }

        [StringLength(50)]
        [Column("phone_number")]
        public string PhoneNumber { get; set; }

        [Column("balance")]
        public decimal Balance { get; set; } = 0;  // Saldo del cliente

        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        public virtual ICollection<OrderEntity> Orders { get; set; }

    }
}
