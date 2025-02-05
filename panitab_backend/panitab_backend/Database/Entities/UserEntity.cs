using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace panitab_backend.Database.Entities
{
    public class UserEntity : IdentityUser
    {
        [StringLength(70, MinimumLength = 3)]
        [Column("first_name")]
        [Required]
        public string FirstName { get; set; }

        // el string length es para que en la base de datos se cree un varchar de 70 caracteres
        [StringLength(70, MinimumLength = 3)]
        [Column("last_name")]
        public string LastName { get; set; }

        //[Column("identity_number")]
        //public string IdentityNumber { get; set; }

        //[Column("is_inactive")]
        //public bool IsInactive { get; set; }

        [Column("refresh_token")]
        [StringLength(450)]
        public string? RefreshToken { get; set; }

        [Column("refresh_token_expire")]
        public DateTime RefreshTokenExpire { get; set; }

        //por si se ocupa
        //[Column("refresh_token")]
        //[StringLength(300)]
        //public string? RefreshToken { get; set; }

        //[Column("refresh-token-date", TypeName = "datetime")]
        //public DateTime RefreshTokenDate { get; set; }

        //[PersonalData]
        //[Column(TypeName = "datetime")]
        //public DateTime CreatedDate { get; set; } = DateTime.UtcNow;


        ////esto es para el reseteo de contraseña
        //public string? PasswordResetToken { get; set; }
        //public DateTime? PasswordResetTokenExpires { get; set; }

    }
}
