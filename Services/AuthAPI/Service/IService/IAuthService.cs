using Services.AuthAPI.Models.DTO;

namespace Services.AuthAPI.Service.IService {
    public interface IAuthUser {
        Task<UserDTO> Register(RegistrationRequestDTO registerReqDto);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginReqDto);
    }
}