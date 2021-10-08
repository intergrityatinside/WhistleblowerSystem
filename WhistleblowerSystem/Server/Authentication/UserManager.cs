using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WhistleblowerSystem.Business.DTOs;
using WhistleblowerSystem.Business.Services;

namespace WhistleblowerSystem.Server.Authentication
{
    public class UserManager
    {
        const string ClaimTypeCompanyId = "CompanyId";

        private readonly UserService _userService;
        public UserManager(UserService userService)
        {
            _userService = userService;
        }

        public async Task<UserDto?> SignInAsync(HttpContext httpContext, string id, string pw)
        {
            UserDto? userDto = null;
            if (await _userService.Authenticate(id, pw))
            {
                userDto = await _userService.FindOneByIdAsync(id);
                if (userDto == null) throw new ArgumentNullException(nameof(userDto));
                ClaimsIdentity identity = new ClaimsIdentity(GetUserClaims(userDto), CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            }
            return userDto;
        }

        public async Task SignOut(HttpContext httpContext)
        {
            await httpContext.SignOutAsync();
        }

        public UserDto? GetUser(IHttpContextAccessor httpContextAccessor)
        {
            var httpContext = httpContextAccessor.HttpContext;
            if (httpContext == null) return null;
            UserDto? userDto = null;
            if (httpContext.User != null
                && httpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier) != null)
            {
                string id = httpContext.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
                string companyId = httpContext.User.Claims.First(x => x.Type == ClaimTypeCompanyId).Value;

                userDto = new UserDto(id, companyId, null);
            }
            return userDto;
        }

        private IEnumerable<Claim> GetUserClaims(UserDto user)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypeCompanyId, user.CompanyId));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id ?? throw new ArgumentNullException(nameof(user.Id))));
            return claims;
        }
    }
}
