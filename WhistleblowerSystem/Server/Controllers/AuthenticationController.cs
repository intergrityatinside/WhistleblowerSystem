using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WhistleblowerSystem.Business.DTOs;
using WhistleblowerSystem.Business.Services;
using WhistleblowerSystem.Server.Authentication;

namespace WhistleblowerSystem.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        readonly UserManager _userManager;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly UserService _userService;

        public AuthenticationController(UserManager userManager,
                        IHttpContextAccessor httpContextAccessor,
                        UserService userService)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }
        
        [HttpGet]
        public async Task<UserDto?> Get()
        {
            var httpUser = _userManager.GetUser(_httpContextAccessor);
            if (httpUser == null) return null;
            return await _userService.FindOneByIdAsync(httpUser.Id);
        }

        [HttpPost("login")]
        public async Task<UserDto> Login(UserDto userDto)
        {
            var user = await _userManager.SignInAsync(HttpContext, userDto.Email, userDto.Password);
            if (user == null) throw new Exception("Login failed");
            return user;
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task Logout()
        {
            await _userManager.SignOut(HttpContext);
        }
    }
}
