using Karadul.Data.DbContexts;
using Karadul.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karadul.Data.Repository.AuthRepositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly KaradulDbContext _context;

        public AuthRepository(KaradulDbContext context)
        {
            _context = context;
        }

        public async Task<Admin> AdminLogin(Admin admin)
        {
            var adminExist = await _context
                .Admins.Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Email == admin.Email && x.Password == admin.Password);
            if (adminExist == null)
            {
                return null;
            }
            return adminExist;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
