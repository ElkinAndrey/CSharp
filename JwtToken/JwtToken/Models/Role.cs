using Microsoft.AspNetCore.Identity;

namespace JwtToken.Models
{
    public class Role : IdentityRole<short>
    {
        public List<User> Users { get; set; }
    }
}
