using Microsoft.AspNetCore.Mvc;
// NOTE: This is for make renderable the SelectListItems
using Microsoft.AspNetCore.Mvc.Rendering;

using Newtonsoft.Json;

using Web.Models.DTO;
using Web.Service.IService;
using Web.Utility;

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

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDTO obj) {
            ResponseDTO res = await _authService.LoginAsync(obj);

            if (res != null && res.IsSuccess) {
                LoginResponseDTO loginResDto = JsonConvert.DeserializeObject<LoginResponseDTO>(Convert.ToString(res.Result));

                return RedirectToAction("Index", "Home");
            }
            else {
                ModelState.AddModelError("CustomError", res.Message);

                return View(obj);
            }
        }


        [HttpGet]
        public IActionResult Register() {
            var roleList = new List<SelectListItem>() {
                new SelectListItem {
                    Text=SD.RoleAdmin,
                    Value = SD.RoleAdmin
                },
                new SelectListItem {
                    Text=SD.RoleCustomer,
                    Value = SD.RoleCustomer
                }
            };

            ViewBag.RoleList = roleList;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDTO obj) {
            ResponseDTO res = await _authService.RegisterAsync(obj);
            ResponseDTO role;

            if (res != null && res.IsSuccess) {
                if (string.IsNullOrEmpty(obj.Role)) obj.Role = SD.RoleCustomer;

                role = await _authService.AssignRoleAsync(obj);

                if (role != null && role.IsSuccess) {
                    TempData["success"] = "Registration successful";
                }

                return RedirectToAction(nameof(Login));
            }

            return View();
        }

        public IActionResult Logout() {
            return View();
        }
    }
}