using Karadul.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karadul.Data.Repository.ProductRepositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> GetWomansProduct();
        Task<IEnumerable<Product>> GetMensProduct();
        Task<Product> CreateAsync(Product entity, int categoryId);
        Task<Product> UpdateAsync(Product entity, int categoryId);

    }
}
