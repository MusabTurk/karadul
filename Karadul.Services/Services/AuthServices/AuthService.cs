using Karadul.Data.Entities;
using Karadul.Data.Repository.AuthRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karadul.Services.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<bool> AdminLogin(Admin admin)
        {
            return await _authRepository.AdminLogin(admin);
        }
    }
}
