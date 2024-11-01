using Datas.Entities;

namespace Repositories.Interfaces
{
    public interface ILoginRepository
    {
        Task<User?> GetUserByUsernameAndPassword(string username, string password);
        Task UpdateUser(User user);
        Task<User> FindUser(string username);
    }
}
