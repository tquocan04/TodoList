using Datas.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface ILoginRepository
    {
        Task<User?> GetUserByUsernameAndPassword(string username, string password);
    }
}
