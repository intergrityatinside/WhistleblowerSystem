using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhistleblowerSystem.Business.DTOs;
using WhistleblowerSystem.Business.Services;
using WhistleblowerSystem.Shared;

namespace WhistleblowerSystem.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly UserService _userService;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, UserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;


        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            await _userService.CreateUserAsync(new UserDto(null, ObjectId.GenerateNewId().ToString(), "1234"));

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
