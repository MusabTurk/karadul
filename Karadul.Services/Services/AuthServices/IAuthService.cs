using Karadul.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karadul.Services.Services.AuthServices
{
    public interface IAuthService
    {
        public Task<bool> AdminLogin(Admin admin);
    }
}
