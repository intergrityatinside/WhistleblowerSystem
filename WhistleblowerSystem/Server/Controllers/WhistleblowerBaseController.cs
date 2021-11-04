using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using WhistleblowerSystem.Server.Authentication;
using WhistleblowerSystem.Server.Models;

namespace WhistleblowerSystem.Server.Controllers
{
    [Authorize]
    public class WhistleblowerBaseController : ControllerBase
    {
        protected IHttpContextAccessor _httpContextAccessor;
        protected WhistleblowerManager _whistleblowerManager;
        protected HttpContextWhistleblower _currentWhistleblower;
        public WhistleblowerBaseController(IHttpContextAccessor httpContextAccessor, WhistleblowerManager whistleblowerManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _whistleblowerManager = whistleblowerManager;
            _currentWhistleblower = whistleblowerManager.GetWhistleblower(httpContextAccessor) ?? throw new Exception("Not Authenticated - please Login");
        }
    }
}
