using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using WhistleblowerSystem.Server.Authentication;
using WhistleblowerSystem.Server.Models;

namespace WhistleblowerSystem.Server.Controllers
{
    public class AuthenticationBaseController : ControllerBase
    {
        protected IHttpContextAccessor _httpContextAccessor;
        protected UserManager _userManager;
        protected HttpContextUser? _currentUser;
        protected WhistleblowerManager _whistleblowerManager;
        protected HttpContextWhistleblower? _currentWhistleblower;
        public AuthenticationBaseController(IHttpContextAccessor httpContextAccessor, UserManager userManager, WhistleblowerManager whistleblowerManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _whistleblowerManager = whistleblowerManager;
            _currentUser = userManager.GetUser(httpContextAccessor);
            _currentWhistleblower = whistleblowerManager.GetWhistleblower(httpContextAccessor);
        }

        public void CheckRight(string formId)
        {
            if (_currentUser == null)
            {
                if (_currentWhistleblower == null || _currentWhistleblower.FormId != formId) throw new Exception("No rights for this form");
            }
        }
    }
}
