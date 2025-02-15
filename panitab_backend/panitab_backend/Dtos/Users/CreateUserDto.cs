using System.ComponentModel.DataAnnotations;

namespace panitab_backend.Dtos.Users
{
    public class CreateUserDto
    {
        [StringLength(70, MinimumLength = 3, ErrorMessage = "Los {0} no peden tener más de {1} y menos de {2} caracteres")]
        [Display(Name = "Nombres")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string FirstName { get; set; }

        [StringLength(70, MinimumLength = 3)]
        [Display(Name = "Apellidos")]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Correo Electronico")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [EmailAddress(ErrorMessage = "El campo {0} no es valido.")]
        public string Email { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [RegularExpression(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "La contraseña debe ser segura y contener al menos 8 caracteres, " +
            "incluyendo minúsculas, mayúsculas, números y caracteres especiales.")]
        public string Password { get; set; }

        [Display(Name = "Confirmar Contraseña.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Compare(nameof(Password), ErrorMessage = "Las conotraseñas no coinciden")]
        public string ConfirmPassword { get; set; }

        //nuevo campo de role
        public string Role { get; set; }
    }
}
