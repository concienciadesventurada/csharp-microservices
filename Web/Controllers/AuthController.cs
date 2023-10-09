using Web.Service.IService;
using Web.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers {
    public class AuthController : Controller {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login() {
            LoginRequestDTO loginReqDto = new();

            return View(loginReqDto);
        }

        [HttpGet]
        public IActionResult Register() {
            return View();
        }
    }
}