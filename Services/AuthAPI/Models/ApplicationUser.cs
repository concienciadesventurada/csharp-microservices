using Microsoft.AspNetCore.Identity;

namespace Services.AuthAPI.Models {
    public class ApplicationUser : IdentityUser {
        public string Name { get; set; } = string.Empty;
    }
}