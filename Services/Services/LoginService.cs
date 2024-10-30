using Repositories.Interfaces;
using Repositories.Repositories;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;

        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }
        public bool IsLoggedIn(string username, string password)
        {
            var user = _loginRepository.GetUserByUsernameAndPassword(username, password);
            if (user == null)
            {
                return false;
            }
            return true;
        }
    }
}
