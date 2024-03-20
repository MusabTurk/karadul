using Karadul.Data.DbContexts;
using Karadul.Data.Entities;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Product> CreateAsync(Product entity, int categoryId)
        {
            var createdProduct = await _repository.CreateAsync(entity);
            var categoryProduct = new CategoryProduct
            {
                CategoryId = categoryId,
                ProductId = createdProduct.Id
            };
            _context.CategoryProducts.Add(categoryProduct);
            await _context.SaveChangesAsync();
            return createdProduct;
        }

        public Task<Product> CreateAsync(Product entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var products = await _repository.GetAllAsync();
            return products;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Product>> GetMensProduct()
        {
            var categoryProducts = await _context.CategoryProducts
                 .Include(x => x.Category)
                 .Include(x => x.Product)
                 .Where(x => x.Category.CategoryName == "Erkek").ToListAsync();

            var productList = new List<Product>();
            foreach (var categoryProduct in categoryProducts)
            {
                var product = await _repository.GetByIdAsync(categoryProduct.ProductId);
                productList.Add(product);
            }


            return productList;
        }

        public async Task<IEnumerable<Product>> GetWomansProduct()
        {
            var categoryProducts = await _context.CategoryProducts
                .Include(x => x.Category)
                .Include(x => x.Product)
                .Where(x => x.Category.CategoryName == "Kadın").ToListAsync();

            var productList = new List<Product>();
            foreach (var categoryProduct in categoryProducts)
            {
                var product = await _repository.GetByIdAsync(categoryProduct.ProductId);
                productList.Add(product);
            }


            return productList;
        }

        public async Task<Product> UpdateAsync(Product entity, int categoryId)
        {
            var updatedProduct = await _repository.UpdateAsync(entity);

            var categoryProduct = await _context.CategoryProducts.FirstOrDefaultAsync(x => x.ProductId == entity.Id);

            if (categoryProduct != null)
            {
                // Eğer Product zaten bir kategoriye aitse, mevcut kaydı güncelle
                categoryProduct.CategoryId = categoryId;
                _context.CategoryProducts.Update(categoryProduct);
            }
            else
            {
                // Eğer Product henüz bir kategoriye ait değilse, yeni bir kayıt ekle
                categoryProduct = new CategoryProduct
                {
                    ProductId = entity.Id,
                    CategoryId = categoryId
                };
                _context.CategoryProducts.Add(categoryProduct);
            }

            await _context.SaveChangesAsync();

            return updatedProduct;
        }

        public Task<Product> UpdateAsync(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
