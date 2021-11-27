using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Net;
using System.Security.Claims;

namespace WhistleblowerSystem.Server.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class CheckRightsAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly string[] _expectedRoles;

        public CheckRightsAttribute(params string[] expectedRoles)
        {
            _expectedRoles = expectedRoles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (user.Identity is not { IsAuthenticated: true })
            {
                context.Result = new UnauthorizedResult();

            }
            else
            {

                var hasRight = false;
                var rightClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
                if (rightClaim != null)
                {
                    hasRight = _expectedRoles.Contains(rightClaim.Value);
                }

                if (!hasRight)
                {
                    context.Result = new ObjectResult($"The following rights are required: {string.Join(", ", _expectedRoles)}")
                    { StatusCode = (int)HttpStatusCode.Forbidden };
                }
            }
        }
    }
}
