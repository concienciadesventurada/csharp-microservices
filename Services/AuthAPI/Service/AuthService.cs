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

        public async Task<UserDTO> Register(RegistrationRequestDTO registerReqDto) {
            ApplicationUser user = new() {
                UserName = registerReqDto.Email,
                Email = registerReqDto.Email,
                NormalizedEmail = registerReqDto.Email.ToUpper(),
                Name = registerReqDto.Name,
                PhoneNumber = registerReqDto.PhoneNumber
            };

            try {
                var res = await _userManager.CreateAsync(user, registerReqDto.Password);

                if (res.Succeeded) {
                    var userRes = _db.ApplicationUsers.First(u => u.UserName == registerReqDto.Email);

                    UserDTO userDto = new() {
                        Email = userRes.Email,
                        ID = userRes.Id,
                        Name = userRes.Name,
                        PhoneNumber = userRes.PhoneNumber
                    };

                    return userDto;
                }
            }
            catch (Exception ex) {
                return new UserDTO();
            }

            return new UserDTO();
        }

        public Task<LoginResponseDTO> Login(LoginRequestDTO loginReqDto) {
            throw new NotImplementedException();
        }
    }
}