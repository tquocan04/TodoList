using AutoMapper;
using Datas.DTOs;
using Datas.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repositories.Interfaces;
using Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Services.Services
{
    public class TokenService : ITokenService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public TokenService(IConfiguration configuration, IMapper mapper, ILoginRepository loginRepository)
        {
            _configuration = configuration;
            _mapper = mapper;
            _loginRepository = loginRepository;
        }
        public string GenerateToken(UserDTO userDTO)
        {
            var expiryMinutes = int.Parse(_configuration["Jwt:TokenExpiredInMinutes"]);
            var claims = new List<Claim>    // các thông tin về người dùng sẽ được lưu trong token JWT
            {
                new Claim(ClaimTypes.Name, userDTO.UserName),
                // Add additional claims as needed (Id, email, role,...)
            };

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokeOptions = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(expiryMinutes),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true, 
                ValidAudience = _configuration["Jwt:Audience"],

                ValidateIssuer = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"])),

                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }

        public bool ValidateRefreshToken(User user, string refreshToken)
        {
            return user.RefreshToken == refreshToken && user.RefreshTokenExpiryTime <= DateTime.Now;
        }
        public async Task<(string AccessToken, string RefreshToken)> RefreshToken(UserDTO userDTO, string refreshToken)
        {
            User user = await _loginRepository.FindUser(userDTO.UserName);
            _mapper.Map(userDTO, user);
            if (!ValidateRefreshToken(user, refreshToken))
            {
                throw new SecurityTokenException("Invalid refresh token");
            }
            
            var newAccessToken = GenerateToken(userDTO);
            var newRefreshToken = GenerateRefreshToken();

            // Cập nhật refresh token trong cơ sở dữ liệu
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(2); // Cấu hình thời gian sống cho refresh token
            
            await _loginRepository.UpdateUser(user);

            return (newAccessToken, newRefreshToken);
        }
    }
}
