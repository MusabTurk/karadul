using Karadul.Data.Entities;

namespace Karadul.Services.Services.AuthServices
{
    public interface IAuthService
    {
        public Task<Admin> AdminLogin(Admin admin);

    }
}
