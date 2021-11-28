using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WhistleblowerSystem.Shared.DTOs;
using WhistleblowerSystem.Business.Services;
using WhistleblowerSystem.Server.Authentication;

namespace WhistleblowerSystem.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : UserBaseController
    {
        private readonly UserService _userService;

        public UserController(UserManager userManager,
            IHttpContextAccessor httpContextAccessor,
            UserService userService) : base(httpContextAccessor, userManager)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<UserDto> Post(UserDto user)
        {
            return await _userService.CreateUserAsync(user);
        }
    }
}
