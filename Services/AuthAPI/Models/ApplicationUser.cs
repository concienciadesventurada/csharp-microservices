using Microsoft.AspNetCore.Identity;

namespace Services.AuthAPI.Models {
    public class ApplicationUser : IdentityUser {
        public double Name { get; set; }
    }
}