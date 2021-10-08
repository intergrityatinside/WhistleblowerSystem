using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhistleblowerSystem.Business.DTOs;
using WhistleblowerSystem.Business.Services;

namespace WhistleblowerSystem.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserService _userService;

        public UserController(ILogger<UserController> logger, UserService userService) {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        public async Task<UserDto> Get()
        {
            return await _userService.CreateUserAsync(new UserDto(null, ObjectId.GenerateNewId().ToString(), "1234"));
        }
    }
}
