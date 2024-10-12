using System.ComponentModel.DataAnnotations;

namespace panitab_backend.Dtos.Auth
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "El nombre es requerido.")]
        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El apellido es requerido.")]
        [StringLength(50, ErrorMessage = "El apellido no puede tener más de 50 caracteres.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El número de identidad es requerido.")]
        [StringLength(20, ErrorMessage = "El número de identidad no puede tener más de 20 caracteres.")]
        public string IdentityNumber { get; set; }

        [Required(ErrorMessage = "El correo electrónico es requerido.")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener entre 6 y 100 caracteres.")]
        public string Password { get; set; }

        public string Role { get; set; }

        public bool IsInactive { get; set; } = false; // Valor predeterminado si no se especifica
    }
}
