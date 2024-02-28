using Karadul.WebMVC.Dtos.ProductDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace Karadul.WebMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactor;

        public ProductController(IHttpClientFactory httpClientFactor)
        {
            _httpClientFactor = httpClientFactor;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactor.CreateClient();
            var reposeMessage = await client.GetAsync("https://localhost:7254/api/product");
            if (reposeMessage.IsSuccessStatusCode)
            {
                var jsonData = await reposeMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ProductDto>>(jsonData);
                return View(values);
            }
            return View();
        }
        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(CreateProductDto createProductDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var client = _httpClientFactor.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createProductDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var reposeMessage = await client.PostAsync("https://localhost:7254/api/product", stringContent);
            if (reposeMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateService(int id)
        {
            var client = _httpClientFactor.CreateClient();
            var reposeMessage = await client.GetAsync($"https://localhost:7254/api/product/{id}");
            if (reposeMessage.IsSuccessStatusCode)
            {
                var jsonData = await reposeMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ProductDto>(jsonData);
                return View(values);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateService(UpdateProductDto updateProductDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var client = _httpClientFactor.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateProductDto);
            StringContent stringContent = new StringContent(jsonData,Encoding.UTF8, "application/json");
            var reposeMessage = await client.PutAsync("https://localhost:7254/api/product", stringContent);
            if (reposeMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> DeleteService(int id)
        {
            var client = _httpClientFactor.CreateClient();
            var reposeMessage = await client.DeleteAsync($"https://localhost:7254/api/product/{id}");
            if (reposeMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}

