using Services.AuthAPI.Models.DTO;
using Services.AuthAPI.Service.IService;

namespace Services.AuthAPI.Service {
    public class AuthService : IAuthUser {
        public Task<UserDTO> Register(RegistrationRequestDTO registerReqDto) {
            throw new NotImplementedException();
        }
        public Task<LoginResponseDTO> Login(LoginRequestDTO loginReqDto) {
            throw new NotImplementedException();
        }
    }
}