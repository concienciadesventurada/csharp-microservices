using Microsoft.AspNetCore.Identity;

using Services.AuthAPI.Data;
using Services.AuthAPI.Models;
using Services.AuthAPI.Models.DTO;
using Services.AuthAPI.Service.IService;

namespace Services.AuthAPI.Service {
    public class AuthService : IAuthUser {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        // NOTE: All these are dependency injection that Identity already
        // provides for managing users
        public AuthService(AppDbContext db,
                UserManager<ApplicationUser> userManager,
                RoleManager<IdentityRole> roleManager
            ) {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public Task<UserDTO> Register(RegistrationRequestDTO registerReqDto) {
            throw new NotImplementedException();
        }
        public Task<LoginResponseDTO> Login(LoginRequestDTO loginReqDto) {
            throw new NotImplementedException();
        }
    }
}