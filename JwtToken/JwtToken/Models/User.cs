using Microsoft.AspNetCore.Identity;

namespace JwtToken.Models
{
    public class User : IdentityUser<Guid>
    {
        public byte[] PasswordSalt { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? TokenCreated { get; set; }
        public DateTime? TokenExpires { get; set; }
        public Role Role { get; set; }
    }
}
