using Microsoft.AspNetCore.Identity;

using Services.AuthAPI.Data;
using Services.AuthAPI.Models;
using Services.AuthAPI.Models.DTO;
using Services.AuthAPI.Service.IService;

namespace Services.AuthAPI.Service {
    public class AuthService : IAuthService {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJWTGenerator _JwtGenerator;

        // NOTE: All these are dependency injection that Identity already
        // provides for managing users
        public AuthService(
                AppDbContext db,
                UserManager<ApplicationUser> userManager,
                RoleManager<IdentityRole> roleManager,
                IJWTGenerator jwtGenerator
            ) {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _JwtGenerator = jwtGenerator;
        }

        // NOTE: Instructor changes it for a string, I imagine for
        // (de)serialization bc I know we have a (de)ser client-side. However, I
        // don't think I'd be implementing it this way using other frontend
        public async Task<string> Register(RegistrationRequestDTO registerReqDto) {
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

                    return "";
                }
                else {
                    return res.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex) {
            }
            // DOUBT: Guy makes it the other way around, why?
            return "Error encountered";
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginReqDto) {
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.UserName == loginReqDto.Email);

            if (user != null) {
                bool isValid = await _userManager.CheckPasswordAsync(user, loginReqDto.Password);

                if (!isValid) return new LoginResponseDTO() { User = null };

                UserDTO userDTO = new() {
                    Email = user.Email,
                    ID = user.Id,
                    Name = user.Name,
                    PhoneNumber = user.PhoneNumber
                };

                var token = _JwtGenerator.GenerateToken(user);

                LoginResponseDTO loginResDto = new LoginResponseDTO() {
                    User = userDTO,
                    Token = token
                };

                return loginResDto;
            }
            else {
                return new LoginResponseDTO() { User = null };
            }
        }
    }
}