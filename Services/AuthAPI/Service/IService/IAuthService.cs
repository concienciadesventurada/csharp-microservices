using Services.AuthAPI.Models.DTO;

namespace Services.AuthAPI.Service.IService {
    public interface IAuthService {
        Task<string> Register(RegistrationRequestDTO registerReqDto);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginReqDto);
    }
}