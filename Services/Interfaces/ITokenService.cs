using Datas.DTOs;
using Datas.Entities;
using System.Security.Claims;

namespace Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(UserDTO userDTO);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        bool ValidateRefreshToken(User user, string refreshToken);
        Task<(string AccessToken, string RefreshToken)> RefreshToken(UserDTO userDTO, string refreshToken);
    }
}
