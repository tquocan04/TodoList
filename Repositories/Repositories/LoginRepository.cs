using Datas;
using Datas.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly Context _context;
        public LoginRepository(Context context) 
        {
            _context = context;
        }

        public async Task<User?> GetUserByUsernameAndPassword(string username, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);
        }
    }
}
