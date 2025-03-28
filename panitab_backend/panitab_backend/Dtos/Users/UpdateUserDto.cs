using System.ComponentModel.DataAnnotations;

namespace panitab_backend.Dtos.Users
{
    public class UpdateUserDto
    {
        [StringLength(70, MinimumLength = 3)]
        public string FirstName { get; set; }

        [StringLength(70, MinimumLength = 3)]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Role { get; set; } // Si el admin quiere cambiar el rol
    }
}
