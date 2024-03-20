using Karadul.Data.DbContexts;
using Karadul.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karadul.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private KaradulDbContext _context;
        private DbSet<T> _table;

        public GenericRepository(KaradulDbContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _table.AddAsync(entity);
            await SaveAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            T entity = await _table.SingleOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                return false;
            }
            _table.Remove(entity);
            await SaveAsync();

            return true;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _table.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _table.FindAsync(id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _table.Update(entity);
            await SaveAsync();
            return entity;
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
