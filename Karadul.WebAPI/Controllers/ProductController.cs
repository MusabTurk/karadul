using AutoMapper;
using Karadul.Data.Entities;
using Karadul.Services.Services.ProductServices;
using Karadul.WebAPI.Models.ProductModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Karadul.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProductAsync()
        {
            var products = await _productService.GetAllAsync();
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }

        [HttpGet("MenProducts")]
        public async Task<IActionResult> GetAllMensProductAsync()
        {
            var products = await _productService.GetMensProduct();
            if (products == null)
            {
                return NotFound();
            }
            var productMap = _mapper.Map<IEnumerable<ProductModel>>(products);
            return Ok(productMap);
        }

        [HttpGet("WomenProducts")]
        public async Task<IActionResult> GetAllWomensProductAsync()
        {
            var products = await _productService.GetWomansProduct();
            if (products == null)
            {
                return NotFound();
            }
            var productMap = _mapper.Map<IEnumerable<ProductModel>>(products);
            return Ok(productMap);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneProductById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductModel createProductModel)
        {
            var productMap = _mapper.Map<Product>(createProductModel);

            //if (File != null && File.Length > 0)
            //{
            //    using (var memoryStream = new MemoryStream())
            //    {
            //        await File.CopyToAsync(memoryStream);
            //        productMap.ProductPicture = memoryStream.ToArray();
            //    }
            //}

            var addProduct = await _productService.CreateAsync(productMap, createProductModel.CategoryId);
            if (addProduct == null)
            {
                return BadRequest();
            }
            return Ok(addProduct);
        }



        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductModel updateProductModel)
        {
            var productMap = _mapper.Map<Product>(updateProductModel);

            //if (product.File != null && product.File.Length > 0)
            //{
            //    using (var memoryStream = new MemoryStream())
            //    {
            //        await product.File.CopyToAsync(memoryStream);
            //        productMap.ProductPicture = memoryStream.ToArray();
            //    }
            //}

            var updateProduct = await _productService.UpdateAsync(productMap, updateProductModel.CategoryId);
            if (updateProduct == null)
            {
                return BadRequest();
            }
            return Ok(updateProduct);
        }

        [Authorize(Roles = "Admin")]
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
