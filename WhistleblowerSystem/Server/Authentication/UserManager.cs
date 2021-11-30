using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WhistleblowerSystem.Shared.DTOs;
using WhistleblowerSystem.Business.Services;
using WhistleblowerSystem.Server.Models;

namespace WhistleblowerSystem.Server.Authentication
{
    public class UserManager
    {
        private readonly UserService _userService;
        public UserManager(UserService userService)
        {
            _userService = userService;
        }

        public async Task<UserDto?> CompanyUserSignInAsync(HttpContext httpContext, string email, string pw)
        {
            UserDto? userDto = null;
            if (await _userService.Authenticate(email, pw))
            {
                userDto = await _userService.FindOneByEmailAsync(email);
                if (userDto == null) throw new ArgumentNullException(nameof(userDto));
                ClaimsIdentity identity = new ClaimsIdentity(GetCompanyUserClaims(userDto), CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            }
            return userDto;
        }

        public async Task SignOut(HttpContext httpContext)
        {
            await httpContext.SignOutAsync();
        }

        public HttpContextUser? GetUser(IHttpContextAccessor httpContextAccessor)
        {
            var httpContext = httpContextAccessor.HttpContext;
            if (httpContext == null) return null;
            HttpContextUser? user = null;
            if (httpContext.User != null
                && httpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value == Roles.CompanyUserRole)
            {
                if (httpContext.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value == Roles.WhistleBlowerRole) return null;

                string id = httpContext.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
                user = new HttpContextUser(id);
            }
            return user;
        }

        private IEnumerable<Claim> GetCompanyUserClaims(UserDto user)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id ?? throw new ArgumentNullException(nameof(user.Id))));
            claims.Add(new Claim(ClaimTypes.Role, Roles.CompanyUserRole));
            return claims;
        }
    }
}
