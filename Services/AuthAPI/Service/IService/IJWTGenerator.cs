using Services.AuthAPI.Models;

namespace Services.AuthAPI.Service.IService {
    public interface IJWTGenerator {
        string GenerateToken(ApplicationUser appUser, IEnumerable<string> roles);
    }
}