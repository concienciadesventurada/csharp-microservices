using Web.Models.DTO;
using Web.Service.IService;
using Web.Utility;

namespace Web.Service {
    public class AuthService : IAuthService {
        public readonly IBaseService _baseService;

        public AuthService(IBaseService baseService) {
            _baseService = baseService;
        }

        public async Task<ResponseDTO?> LoginAsync(LoginRequestDTO loginReqDto) {
            return await _baseService.SendAsync(new RequestDTO() {
                ApiType = SD.ApiType.POST,
                Data = loginReqDto,
                Url = SD.AuthAPIBase + "/api/auth/login"
            });
        }

        public async Task<ResponseDTO?> RegisterAsync(RegistrationRequestDTO registerReqDto) {
            return await _baseService.SendAsync(new RequestDTO() {
                ApiType = SD.ApiType.POST,
                Data = registerReqDto,
                Url = SD.AuthAPIBase + "/api/auth/register"
            });
        }

        public async Task<ResponseDTO?> AssignRoleAsync(RegistrationRequestDTO registerReqDto) {
            return await _baseService.SendAsync(new RequestDTO() {
                ApiType = SD.ApiType.POST,
                Data = registerReqDto,
                Url = SD.AuthAPIBase + "/api/auth/assign-role"
            });
        }
    }
}