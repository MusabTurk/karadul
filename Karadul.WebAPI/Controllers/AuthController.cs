using Karadul.Data.Entities;
using Karadul.Services.Services.AuthServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Karadul.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> AdminLogin(Admin admin)
        {
            var adminLogin = await _authService.AdminLogin(admin);
            if (adminLogin == false)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
