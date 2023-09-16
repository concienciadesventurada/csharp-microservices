using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Services.AuthAPI.Service.IService;
using Services.AuthAPI.Models.DTO;

namespace Services.AuthAPI.Controllers {
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase {
        private readonly IAuthService _authService;
        protected ResponseDTO _res;

        public AuthAPIController(IAuthService authService) {
            _authService = authService;
            _res = new();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO model) {
            var errorMsg = await _authService.Register(model);

            if (!string.IsNullOrEmpty(errorMsg)) {
                _res.IsSuccess = false;
                _res.Message = errorMsg;

                return BadRequest(_res);
            }

            return Ok(_res);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login() {
            return Ok();
        }
    }
}