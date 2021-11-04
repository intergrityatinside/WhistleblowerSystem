using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhistleblowerSystem.Business.DTOs;
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
