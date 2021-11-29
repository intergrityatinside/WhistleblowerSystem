using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WhistleblowerSystem.Business.Services;
using WhistleblowerSystem.Server.Authentication;
using WhistleblowerSystem.Shared.DTOs;

namespace WhistleblowerSystem.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        readonly UserManager _userManager;
        readonly WhistleblowerManager _whistleblowerManager;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly UserService _userService;
        readonly WhistleblowerService _whistleblowerService;

        public AuthenticationController(UserManager userManager,
                        WhistleblowerManager whistleblowerManager,
                        IHttpContextAccessor httpContextAccessor,
                        UserService userService,
                        WhistleblowerService whistleblowerService)
        {
            _userManager = userManager;
            _whistleblowerManager = whistleblowerManager;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _whistleblowerService = whistleblowerService;
        }
        
        [HttpGet("user")]
        public async Task<UserDto?> GetCompanyUser()
        {
            var httpUser = _userManager.GetUser(_httpContextAccessor);
            if (httpUser == null) return null;
            return await _userService.FindOneByIdAsync(httpUser.Id);
        }

        [HttpGet("whistleblower")]
        public async Task<WhistleblowerDto?> GetWhistleblower()
        {
            var httpWhistleblower = _whistleblowerManager.GetWhistleblower(_httpContextAccessor);
            if (httpWhistleblower == null) return null;
            return await _whistleblowerService.FindOneByFormIdAsync(httpWhistleblower.FormId);
        }


        [HttpPost("user/login")]
        public async Task<UserDto> Login(UserDto userDto)
        {
            var user = await _userManager.CompanyUserSignInAsync(HttpContext, userDto.Email, userDto.Password);
            if (user == null) throw new Exception("Login failed");
            return user;
        }

        [HttpPost("whistleblower/login")]
        public async Task<WhistleblowerDto?> Login(WhistleblowerDto whistleblower)
        {
            var whistleblowerDto = await _whistleblowerManager.SignInAsync(HttpContext, whistleblower.FormId, whistleblower.Password);
            if (whistleblowerDto == null) throw new Exception("Login failed");
            return whistleblowerDto;
        }

        [HttpPost("logout")]
        public async Task Logout()
        {
            await _userManager.SignOut(HttpContext);
            await _whistleblowerManager.SignOut(HttpContext);
        }
    }
}
