using Karadul.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karadul.Data.Repository.AuthRepositories
{
    public interface IAuthRepository
    {
        public Task<Admin> AdminLogin(Admin admin);
        public Task SaveAsync();
    }
}
