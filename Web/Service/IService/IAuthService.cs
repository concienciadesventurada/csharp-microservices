using Web.Models.DTO;

namespace Web.Service.IService {
    public interface IAuthService {
        Task<ResponseDTO?> LoginAsync(LoginRequestDTO loginReqDto);
        Task<ResponseDTO?> RegisterAsync(RegistrationRequestDTO registerReqDto);
        Task<ResponseDTO?> AssignRoleAsync(RegistrationRequestDTO registerReqDto);
    }
}