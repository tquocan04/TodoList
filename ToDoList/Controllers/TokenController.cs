using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Services;
using TodoItem.Models;

namespace TodoItem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly ILoginService _loginService;

        public TokenController(ITokenService tokenService, ILoginService loginService)
        {
            _tokenService = tokenService;
            _loginService = loginService;
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(TokenAPIModel tokenApiModel)
        {
            if (tokenApiModel is null)
                return BadRequest("Invalid client request");
            string accessToken = tokenApiModel.AccessToken;
            string refreshToken = tokenApiModel.RefreshToken;

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
            if (principal == null || principal.Identity?.Name == null)
            {
                return BadRequest("Invalid access token");
            }
            var username = principal.Identity.Name; //this is mapped to the Name claim by default
            var user = await _loginService.FindByUsername(username);
            if (user == null)
            {
                return BadRequest("User not found");
            }
            var tokens = await _tokenService.RefreshToken(user, refreshToken);

            return Ok(new AuthResponse
            {
                Token = tokens.AccessToken,
                RefreshToken = tokens.RefreshToken
            });
            
        }
    }
}
