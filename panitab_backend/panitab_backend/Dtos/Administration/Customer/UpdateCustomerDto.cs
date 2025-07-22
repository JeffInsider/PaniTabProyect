using System.ComponentModel.DataAnnotations;

namespace panitab_backend.Dtos.Administration.Customer
{
    public class UpdateCustomerDto
    {
        [Required]
        public string IdentityNumber { get; set; } // Número de identidad del cliente

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string PhoneNumber { get; set; }

        [Required]
        public decimal Balance { get; set; } // Saldo del cliente


        public bool IsActive { get; set; }
    }
}
