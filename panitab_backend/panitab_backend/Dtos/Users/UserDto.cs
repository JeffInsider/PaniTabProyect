namespace panitab_backend.Dtos.Users
{
    public class UserDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }

        //saber si esta activo o no
        public bool IsActive { get; set; }
    }
}
