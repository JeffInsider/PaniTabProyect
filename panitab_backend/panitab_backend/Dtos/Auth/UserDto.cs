using System.ComponentModel.DataAnnotations.Schema;

namespace panitab_backend.Dtos.Auth
{
    public class UserDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string IdentityNumber { get; set; }

        public bool IsInactive { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
