﻿namespace panitab_backend.Dtos.Auth
{
    public class LoginResponseDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpiration { get; set; }
        public string RefreshToken { get; set; }
        //public string Email { get; set; }
        //public string Username { get; set; }
        //public string Token { get; set; }
        //public string? RefreshToken { get; set; }
        //public DateTime TokenExpiration { get; set; }
        //public List<string> Roles { get; set; }
    }
}
