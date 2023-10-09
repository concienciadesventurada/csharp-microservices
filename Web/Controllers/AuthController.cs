using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
        private readonly ITokenService _tokenService;

        public AuthController(IAuthService authService, ITokenService tokenService) {
            _authService = authService;
            _tokenService = tokenService;
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

                await SignInUser(loginResDto);

                _tokenService.SetToken(loginResDto.Token);

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

        public async Task<IActionResult> Logout() {
            await HttpContext.SignOutAsync();

            _tokenService.ClearToken();

            return RedirectToAction("Index", "Home");
        }

        private async Task SignInUser(LoginResponseDTO model) {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(model.Token);
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));

            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));

            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));

            // NOTE: Notice how I'm passing the type role itselft instead of
            // another field like the ones before
            identity.AddClaim(new Claim(ClaimTypes.Role,
                jwt.Claims.FirstOrDefault(u => u.Type == "role").Value));

            // NOTE: Same with Name, in this case, the ClaimType match allows to
            // pass properties and to be consumed on .cshtml User.Identity prop
            identity.AddClaim(new Claim(ClaimTypes.Name,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}