using Karadul.Data.Entities;
using Karadul.Data.Repository.ProductRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karadul.Services.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        //public async Task<Product> CreateAsync(Product product)
        //{
        //    return await _productRepository.CreateAsync(product);
        //}

        public async Task<Product> CreateAsync(Product entity, int categoryId)
        {
            return await _productRepository.CreateAsync(entity, categoryId);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _productRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Product>> GetMensProduct()
        {
            return await _productRepository.GetMensProduct();
        }

        public async Task<IEnumerable<Product>> GetWomansProduct()
        {
            return await _productRepository.GetWomansProduct();

        }

        public async Task<Product> UpdateAsync(Product product, int categoryId)
        {
            return await _productRepository.UpdateAsync(product, categoryId);

        }
    }
}
