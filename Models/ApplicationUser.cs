using Microsoft.AspNetCore.Identity;

namespace ManicureShop.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
    }
}
