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

        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        [Column("refresh_token")]
        [StringLength(450)]
        public string? RefreshToken { get; set; }

        [Column("refresh_token_expire")]
        public DateTime RefreshTokenExpire { get; set; }

    }
}