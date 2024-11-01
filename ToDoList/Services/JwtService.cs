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
            var claims = new List<Claim>    // các thông tin về người dùng sẽ được lưu trong token JWT
            {
                new Claim(ClaimTypes.Name, userDTO.UserName),
                // Add additional claims as needed (Id, email, role,...)
            };

            // Tao khoa bao mat doi xung dua tren Secret (lay tu JwtSettings). dc dung de ky token
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),   //danh tinh nguoi dung thong qua claims 
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpiredInMinutes),
                SigningCredentials = credentials,
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
            };

            var tokenHandler = new JwtSecurityTokenHandler();//xu ly (tao va ma hoa) token
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);//tao token theo thong tin tokenDescriptor
            return tokenHandler.WriteToken(securityToken);//token->string

        }
    }
}
