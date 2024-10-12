﻿using System.ComponentModel.DataAnnotations;

namespace panitab_backend.Dtos.Auth
{
    public class LoginDto
    {
        [Display(Name = "Nombre de usuario")]
        [Required(ErrorMessage = "El {0} es obligatorio")]
        public string UserName { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "La {0} es obligatoria")]
        public string Password { get; set; }
    }
}
