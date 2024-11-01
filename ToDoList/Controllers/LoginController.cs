using AutoMapper;
using Datas.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using TodoItem.Models;

namespace TodoItem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly ILoginService _loginService;

        public LoginController(ITokenService tokenService, ILoginService loginService)
        {
            _tokenService = tokenService;
            _loginService = loginService;
        }
        [HttpPost]
        public async Task<ActionResult> Login(UserDTO userDTO)
        {
            var user = await _loginService.IsLoggedIn(userDTO);
            if (user == null)
            {
                return BadRequest("Invalid user");
            }
            var accessToken = _tokenService.GenerateToken(userDTO);
            

            return Ok(new AuthResponse
            {
                Token = accessToken,
                RefreshToken = user.RefreshToken
            });
        }

    }
}
