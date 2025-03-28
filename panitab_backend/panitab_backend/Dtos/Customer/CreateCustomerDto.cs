using System.ComponentModel.DataAnnotations;

namespace panitab_backend.Dtos.Customer
{
    public class CreateCustomerDto
    {
        [Required]
        [StringLength(50)]
        public string IdentityNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string PhoneNumber { get; set; }
    }
}
