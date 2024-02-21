using Karadul.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karadul.Services.Services.ProductServices
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product> GetByIdAsync(int id);
        Task<Product> CreateAsync(Product product);
        Task<Product> UpdateAsync(Product product);
        Task<bool> DeleteAsync(int id);

    }
}
