using Karadul.Data.Entities;
using Karadul.Services.Services.ProductServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Karadul.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProductAync()
        {
            var products = await _productService.GetAllAsync();
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneProductById(int id)
        {
            var product = await _productService.GetByIdAsync( id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            var addProduct = await _productService.CreateAsync(product);
            if (addProduct == null)
            {
                return BadRequest();
            }
            return Ok(addProduct);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody]Product product)
        {
            var updateProduct = await _productService.UpdateAsync(product);
            if (updateProduct == null)
            {
                return BadRequest();
            }
            return Ok(updateProduct);
        }
        [HttpDelete]

        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deleteProduct = await _productService.DeleteAsync(id);
            if (!deleteProduct)
            {
                return BadRequest();
            }
            return Ok(deleteProduct);
        }
    }
}

