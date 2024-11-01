using AutoMapper;
using Datas.DTOs;
using Datas.Entities;
using Repositories.Interfaces;
using Repositories.Repositories;
using Services.Interfaces;

namespace Services.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public LoginService(ILoginRepository loginRepository, ITokenService tokenService, IMapper mapper)
        {
            _loginRepository = loginRepository;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<UserDTO> FindByUsername(string username)
        {
            var user = await _loginRepository.FindUser(username);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> IsLoggedIn(UserDTO userDTO)
        {
            if (userDTO == null) return null;
            var user = await _loginRepository.GetUserByUsernameAndPassword(userDTO.UserName, userDTO.Password);
            if (user == null)
            {
                return null;
            }
            user.RefreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(2); // refresh token có hạn 2 phút
            await _loginRepository.UpdateUser(user);
            return _mapper.Map<UserDTO>(user);
        }
    }
}
