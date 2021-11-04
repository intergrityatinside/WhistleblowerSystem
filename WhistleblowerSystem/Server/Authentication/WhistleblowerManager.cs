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
using WhistleblowerSystem.Server.Models;

namespace WhistleblowerSystem.Server.Authentication
{
    public class WhistleblowerManager
    {
        const string ClaimTypeFormId = "FormId";

        private readonly WhistleblowerService _whistleblowerService;
        public WhistleblowerManager(WhistleblowerService whistleblowerService)
        {
            _whistleblowerService = whistleblowerService;
        }

        public async Task<WhistleblowerDto?> SignInAsync(HttpContext httpContext, string formId, string pw)
        {
            WhistleblowerDto? whistleblowerDto = null;
            if (await _whistleblowerService.Authenticate(formId, pw))
            {
                whistleblowerDto = await _whistleblowerService.FindOneByFormIdAsync(formId);
                if (whistleblowerDto == null) throw new ArgumentNullException(nameof(whistleblowerDto));
                ClaimsIdentity identity = new ClaimsIdentity(GetWhistleblowerClaims(whistleblowerDto), CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            }
            return whistleblowerDto;
        }

        public async Task SignOut(HttpContext httpContext)
        {
            await httpContext.SignOutAsync();
        }

        public HttpContextWhistleblower? GetWhistleblower(IHttpContextAccessor httpContextAccessor)
        {
            var httpContext = httpContextAccessor.HttpContext;
            if (httpContext == null) return null;
            HttpContextWhistleblower? whistleblower = null;
            if (httpContext.User != null
                && httpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier) != null)
            {
                string id = httpContext.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
                string formId = httpContext.User.Claims.First(x => x.Type == ClaimTypeFormId).Value;
                whistleblower = new HttpContextWhistleblower(id, formId);
            }
            return whistleblower;
        }

        private IEnumerable<Claim> GetWhistleblowerClaims(WhistleblowerDto whistleblower)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, whistleblower.Id ?? throw new ArgumentNullException(nameof(whistleblower.Id))));
            return claims;
        }
    }
}
