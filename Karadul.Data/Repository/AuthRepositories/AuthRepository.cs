using Karadul.Data.Contexts;
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
        public async Task<bool> AdminLogin(Admin admin)
        {
            var adminExist = await _context .Admins .FirstOrDefaultAsync( x => x.Email == admin.Email && x.Password == admin.Password );
            if (adminExist == null) 
            { 
            return false;
            }
            return true;    
        }
    }
}
