using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using WhistleblowerSystem.Business.DTOs;
using WhistleblowerSystem.Server.Authentication;
using WhistleblowerSystem.Server.Models;

namespace WhistleblowerSystem.Server.Controllers
{
    public class UserBaseController : ControllerBase
    {
        protected IHttpContextAccessor _httpContextAccessor;
        protected UserManager _userManager;
        protected HttpContextUser? _currentUser;
        protected WhistleblowerDto? _whistleblower;
        public UserBaseController(IHttpContextAccessor httpContextAccessor, UserManager userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _currentUser = userManager.GetUser(httpContextAccessor);
            _whistleblower = userManager.GetWhistleBlower(httpContextAccessor);
        }

        public void CheckRight(string formId)
        {
            if(_currentUser == null)
            {
                if (_whistleblower == null || _whistleblower.FormId != formId) throw new Exception("No rights for this form");
            }
        }
    }
}
