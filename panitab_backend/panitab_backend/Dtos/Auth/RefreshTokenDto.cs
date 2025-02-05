using System.ComponentModel.DataAnnotations;

namespace panitab_backend.Dtos.Auth
{
    public class RefreshTokenDto
    {
        [Required(ErrorMessage = "El yoken es requerido.")]
        public string Token { get; set; }

        [Required(ErrorMessage = "El refresh token es requerido.")]
        public string RefreshToken { get; set; }
        //public string Token { get; set; }

        //public string RefreshToken { get; set; }
    }
}
