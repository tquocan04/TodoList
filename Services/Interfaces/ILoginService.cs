using Datas.DTOs;

namespace Services.Interfaces
{
    public interface ILoginService
    {
        Task<UserDTO> IsLoggedIn(UserDTO userDTO);
        Task<UserDTO> FindByUsername(string username);
    }
}
