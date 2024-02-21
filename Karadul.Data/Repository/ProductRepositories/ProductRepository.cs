using Karadul.Data.Contexts;
using Karadul.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karadul.Data.Repository.ProductRepositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IGenericRepository<Product> _repository;
        private readonly KaradulDbContext _context;

        public ProductRepository(IGenericRepository<Product> repository, KaradulDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<Product> CreateAsync(Product entity)
        {
            return await _repository.CreateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public Task<Product> UpdateAsync(Product entity)
        {
            return _repository.UpdateAsync(entity); 
        }
    }
}
