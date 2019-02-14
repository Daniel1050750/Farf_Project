using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Farf_Project.Core;

namespace Farf_Project.Web
{
    public class TokenManagerMiddleware : IMiddleware
    {
        private readonly ITokenManager tokenManager;

        public TokenManagerMiddleware(ITokenManager tokenManager)
        {
            this.tokenManager = tokenManager;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {

            // only verify the token if the user is authenticated
            if(context.User.Identity.IsAuthenticated)
            {
                if (await this.tokenManager.IsCurrentTokenValidAsync())
                {
                    await next(context);

                    return;
                }

                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            else
            {
                await next(context);

                return;
            }
        }
    }
}

