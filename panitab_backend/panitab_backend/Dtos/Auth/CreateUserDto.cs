using System.ComponentModel.DataAnnotations;

namespace panitab_backend.Dtos.Auth
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

        //[Required(ErrorMessage = "El nombre es requerido.")]
        //[StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres.")]
        //public string FirstName { get; set; }

        //[Required(ErrorMessage = "El apellido es requerido.")]
        //[StringLength(50, ErrorMessage = "El apellido no puede tener más de 50 caracteres.")]
        //public string LastName { get; set; }

        //[Required(ErrorMessage = "El número de identidad es requerido.")]
        //[StringLength(20, ErrorMessage = "El número de identidad no puede tener más de 20 caracteres.")]
        //public string IdentityNumber { get; set; }

        //[Required(ErrorMessage = "El correo electrónico es requerido.")]
        //[EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
        //public string Email { get; set; }

        //[Required]
        //[StringLength(50)]
        //public string UserName { get; set; }

        //[Required(ErrorMessage = "La contraseña es requerida.")]
        //[StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener entre 6 y 100 caracteres.")]
        //public string Password { get; set; }

        //public string Role { get; set; }

        //public bool IsInactive { get; set; } = false; // Valor predeterminado si no se especifica
    }
}
