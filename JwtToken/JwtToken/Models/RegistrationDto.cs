﻿namespace JwtToken.Models
{
    public class RegistrationDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public short RoleId { get; set; } = default;
    }
}
