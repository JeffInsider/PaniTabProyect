using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace panitab_backend.Database.Entities
{
    [Table("users")]
    public class UserEntity : IdentityUser
    {
        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("identity_number")]
        public string IdentityNumber { get; set; }

        [Column("is_inactive")]
        public bool IsInactive { get; set; }

        //por si se ocupa
        [Column("refresh_token")]
        [StringLength(300)]
        public string? RefreshToken { get; set; }

        [Column("refresh-token-date", TypeName = "datetime")]
        public DateTime RefreshTokenDate { get; set; }

        [PersonalData]
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public string PasswordResetToken { get; set; }
        public DateTime? PasswordResetTokenExpires { get; set; }

    }
}
