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
    public class WhistleblowerController : WhistleblowerBaseController
    {
        private readonly WhistleblowerService _whistleblowerService;

        public WhistleblowerController(WhistleblowerManager whistleblowerManager,
            IHttpContextAccessor httpContextAccessor,
            WhistleblowerService whistleblowerService) : base(httpContextAccessor, whistleblowerManager)
        {
            _whistleblowerService = whistleblowerService;
        }

        [HttpPost]
        public async Task<WhistleblowerDto> Post(WhistleblowerDto whistleblower)
        {
            return await _whistleblowerService.CreateWhistleblowerAsync(whistleblower);
        }
    }
}
