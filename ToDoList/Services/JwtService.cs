using Datas.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoItem.Models;

namespace TodoItem.Services
{
    public class JwtService
    {
        private readonly JwtSettings _jwtSettings;
        public JwtService(IConfiguration configuration)
        {
            _jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>();
        }

        public string GenerateToken(UserDTO userDTO)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userDTO.UserName),
                // Add additional claims as needed (Id, email, role,...)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpiredInMinutes),
                SigningCredentials = credentials,
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);

        }
    }
}
