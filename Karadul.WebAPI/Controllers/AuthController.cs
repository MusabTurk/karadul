using AutoMapper;
using Karadul.Data.Entities;
using Karadul.Services.Services.AuthServices;
using Karadul.WebAPI.Models.AdminModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Karadul.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        public AuthController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AdminLogin(LoginModel loginModel)
        {
            var adminMap = _mapper.Map<Admin>(loginModel);
            var adminLogin = await _authService.AdminLogin(adminMap);
            if (adminLogin == null)
            {
                return BadRequest();
            }
            return Ok(adminLogin.AccessToken);
        }
    }
}
