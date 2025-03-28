using System.ComponentModel.DataAnnotations;

namespace panitab_backend.Dtos.Customer
{
    public class UpdateCustomerDto
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string PhoneNumber { get; set; }

        public bool IsActive { get; set; }
    }
}
