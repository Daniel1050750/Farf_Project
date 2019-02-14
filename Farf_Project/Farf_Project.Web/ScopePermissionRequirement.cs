using Microsoft.AspNetCore.Authorization;
using Farf_Project.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Farf_Project.Web
{
    public class ScopePermissionRequirement : IAuthorizationRequirement
    {
        public string Scope { get; private set; }

        public ScopePermissionRequirement(string scope)
        {
            this.Scope = scope;
        }
    }

    // https://www.jerriepelser.com/blog/creating-dynamic-authorization-policies-aspnet-core/
    // https://docs.microsoft.com/en-us/aspnet/core/security/authorization/policies?view=aspnetcore-2.2
    public class ScopePermissionHandler : AuthorizationHandler<ScopePermissionRequirement>
    {
        private static string CLAIM_SCOPE = "scope";

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ScopePermissionRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type.Equals(CLAIM_SCOPE)))
            {
                return Task.CompletedTask;
            }
            // verify if this scope is avaiable in this claim
            var isScopeAuthorized = context.User.FindFirst(c => c.Type == "scope").Value.Contains(requirement.Scope);

            if(isScopeAuthorized)  // tem access
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
