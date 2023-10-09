using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using Services.AuthAPI.Models;
using Services.AuthAPI.Service.IService;

namespace Services.AuthAPI.Service {
    public class JWTGenerator : IJWTGenerator {
        private readonly JwtOptions _jwtOptions;

        // NOTE: To have access to the configuration declared in the Program.cs
        // where we Configure<JwtOptions> we have to pass it as IOptions and get
        // the Value property
        public JWTGenerator(IOptions<JwtOptions> jwtOptions) {
            _jwtOptions = jwtOptions.Value;
        }

        public string GenerateToken(ApplicationUser appUser) {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Email, appUser.Email),
                new Claim(JwtRegisteredClaimNames.Sub, appUser.Id),
                new Claim(JwtRegisteredClaimNames.Name, appUser.UserName),
            };

            var tokenDescriptor = new SecurityTokenDescriptor {
                Audience = _jwtOptions.Audience,
                Issuer = _jwtOptions.Issuer,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}