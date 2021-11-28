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
        const string ClaimTypeCompanyId = "CompanyId";
        const string ClaimTypeFormId = "FormId";

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

        public async Task<WhistleblowerDto?> WhistleBlowerSignInAsync(HttpContext httpContext, string formId, string pw)
        {
            var authResult = await _userService.AuthenticateWhilsteblower(formId, pw);
            WhistleblowerDto? whistleBlower = null;
            if (authResult.Item1)
            {
                 whistleBlower = authResult.Item2;
                if (whistleBlower == null) throw new ArgumentNullException(nameof(whistleBlower));
                ClaimsIdentity identity = new ClaimsIdentity(GetWhistleblowerClaims(whistleBlower), CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            }
            return whistleBlower;
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
                && httpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier) != null)
            {
                if (httpContext.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value == Roles.WhistleBlowerRole) return null;

                string id = httpContext.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
                string companyId = httpContext.User.Claims.First(x => x.Type == ClaimTypeCompanyId).Value;
                user = new HttpContextUser(id, companyId);
            }
            return user;
        }

        public WhistleblowerDto? GetWhistleBlower(IHttpContextAccessor httpContextAccessor)
        {
            var httpContext = httpContextAccessor.HttpContext;
            if (httpContext == null) return null;
            WhistleblowerDto? whistleblower = null;
            if (httpContext.User != null
                && httpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier) != null)
            {
                if (httpContext.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value == Roles.CompanyUserRole) return null;

                string id = httpContext.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
                string formId = httpContext.User.Claims.First(x => x.Type == ClaimTypeFormId).Value;
                whistleblower = new WhistleblowerDto(id, formId, "");
            }
            return whistleblower;
        }

        private IEnumerable<Claim> GetCompanyUserClaims(UserDto user)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypeCompanyId, user.CompanyId));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id ?? throw new ArgumentNullException(nameof(user.Id))));
            claims.Add(new Claim(ClaimTypes.Role, Roles.CompanyUserRole));
            return claims;
        }

        private IEnumerable<Claim> GetWhistleblowerClaims(WhistleblowerDto whistleblower)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypeFormId, whistleblower.FormId));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, whistleblower.Id!.ToString()));
            claims.Add(new Claim(ClaimTypes.Role, Roles.WhistleBlowerRole));
            return claims;
        }
    }
}
