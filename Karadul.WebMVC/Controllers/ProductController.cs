using AutoMapper;
using Karadul.Data.Entities;
using Karadul.WebMVC.Dtos.ProductDtos;
using Karadul.WebMVC.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;

namespace Karadul.WebMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public ProductController(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            GetTokenFromSession.AddAuthorizationHeader(client, _httpContextAccessor);
            var responseMessage = await client.GetAsync("https://localhost:7254/api/Product");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ProductDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        public async Task<IActionResult> Womans()
        {
            var client = _httpClientFactory.CreateClient();
            GetTokenFromSession.AddAuthorizationHeader(client, _httpContextAccessor);
            var responseMessage = await client.GetAsync("https://localhost:7254/api/Product/WomenProducts");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ProductDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        public async Task<IActionResult> Mens()
        {
            var client = _httpClientFactory.CreateClient();
            GetTokenFromSession.AddAuthorizationHeader(client, _httpContextAccessor);
            var responseMessage = await client.GetAsync("https://localhost:7254/api/Product/MenProducts");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ProductDto>>(jsonData);
                return View(values);
            }
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromForm] CreateProductDto createProductDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var client = _httpClientFactory.CreateClient();
            GetTokenFromSession.AddAuthorizationHeader(client, _httpContextAccessor);

            if (createProductDto.File != null && createProductDto.File.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await createProductDto.File.CopyToAsync(memoryStream);
                    createProductDto.ProductPicture = memoryStream.ToArray();
                }
            }

            var jsonData = JsonConvert.SerializeObject(createProductDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7254/api/Product/", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Ürün Başarıyla Eklendi";
                return RedirectToAction("AddProduct");
            }
            else
            {
                var errorMessage = await responseMessage.Content.ReadAsStringAsync();
                ModelState.AddModelError("", errorMessage);
                return View(createProductDto);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            var client = _httpClientFactory.CreateClient();
            GetTokenFromSession.AddAuthorizationHeader(client, _httpContextAccessor);
            var responseMessage = await client.GetAsync($"https://localhost:7254/api/Product/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateProductDto>(jsonData);
                return View(values);
            }
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateServiceDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var client = _httpClientFactory.CreateClient();
            GetTokenFromSession.AddAuthorizationHeader(client, _httpContextAccessor);
            if (updateServiceDto.File != null && updateServiceDto.File.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await updateServiceDto.File.CopyToAsync(memoryStream);
                    updateServiceDto.ProductPicture = memoryStream.ToArray();
                }
            }
            var jsonData = JsonConvert.SerializeObject(updateServiceDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7254/api/Product/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Ürün Başarıyla Güncellendi";

                return RedirectToAction("UpdateProduct");
            }
            else
            {
                var errorMessage = await responseMessage.Content.ReadAsStringAsync();
                ModelState.AddModelError("", errorMessage);
                return View(updateServiceDto);
            }
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var client = _httpClientFactory.CreateClient();
            GetTokenFromSession.AddAuthorizationHeader(client, _httpContextAccessor);
            var responseMessage = await client.DeleteAsync($"https://localhost:7254/api/Product?id={id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Ürün başarıyla silindi.";
            }
            else
            {
                TempData["ErrorMessage"] = "Ürün silinirken bir hata oluştu.";
            }

            // Kullanıcıyı herhangi bir sayfaya yönlendirin, örneğin anasayfaya.
            return Redirect(HttpContext.Request.Headers["Referer"].ToString());
        }
    }

}
