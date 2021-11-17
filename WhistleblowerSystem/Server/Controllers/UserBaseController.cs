using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using WhistleblowerSystem.Server.Authentication;
using WhistleblowerSystem.Server.Models;

namespace WhistleblowerSystem.Server.Controllers
{
    public class UserBaseController : ControllerBase
    {
        protected IHttpContextAccessor _httpContextAccessor;
        protected UserManager _userManager;
        protected HttpContextUser? _currentUser;
        public UserBaseController(IHttpContextAccessor httpContextAccessor, UserManager userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _currentUser = userManager.GetUser(httpContextAccessor);
        }
    }
}
