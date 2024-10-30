using Datas.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using TodoItem.Services;

namespace TodoItem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly ILoginService _loginService;

        public LoginController(JwtService jwtService, ILoginService loginService)
        {
            _jwtService = jwtService;
            _loginService = loginService;
        }
        [HttpPost("/login")]
        public async Task<ActionResult> Login(UserDTO userDTO)
        {
            bool check = _loginService.IsLoggedIn(userDTO.UserName, userDTO.Password);
            if (!check)
            {
                return BadRequest();
            }
            return Ok(_jwtService.GenerateToken(userDTO));
        }

    }
}
