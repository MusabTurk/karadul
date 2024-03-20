using Karadul.WebMVC.Dtos.LoginDtos;
using Karadul.WebMVC.Dtos.ProductDtos;
using Karadul.WebMVC.Helpers;
using Karadul.WebMVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace Karadul.WebMVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdminController(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
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


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(loginDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7254/api/Auth", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseContent = await responseMessage.Content.ReadAsStringAsync();
                var token = responseContent;
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email,loginDto.Email),
                    new Claim(ClaimTypes.Role,"Admin")
                };

                var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties();

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity), authProperties);
                HttpContext.Session.SetString("Token", token);
                return RedirectToAction("Index", "Admin");

            }
            ModelState.AddModelError("HATA!", "Giriş bilgileri geçersiz. Lütfen tekrar deneyin.");

            return View();
        }
        // WOMEN PRODUCT

        [HttpGet]
        public async Task<IActionResult> WomenProduct()
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

      
        // MEN PRODUCT

        [HttpGet]
        public async Task<IActionResult> MenProduct()
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
 
    }

}