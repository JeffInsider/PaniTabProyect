namespace panitab_backend.Dtos.Auth
{
    public class UpdateUserDto
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }

        public string IdentityNumber { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public bool IsInactive { get; set; }
    }
}
