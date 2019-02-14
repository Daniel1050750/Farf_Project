using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Farf_Project.Core;

namespace Farf_Project.Web
{ 
    public class TokenManager : ITokenManager
    {
        #region Private Properties

        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ISessionTokenRepository sessionTokenRepository;

        #endregion

        #region Constructor

        public TokenManager(IHttpContextAccessor httpContextAccessor, ISessionTokenRepository sessionTokenRepository)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.sessionTokenRepository = sessionTokenRepository;
        }

        #endregion

        #region Public Methods

        public async Task StoreTokenAsync(string token, DateTime expirationDate, User user)
        {
            await this.sessionTokenRepository.SaveTokenAsync(token, expirationDate, user);
        }

        public async Task<bool> IsCurrentTokenValidAsync()
        {
            return await this.IsTokenValidAsync(this.GetCurrentToken());
        }

        public async Task InvalidateCurrentTokenAsync()
        {
            await this.InvalidateTokenAsync(this.GetCurrentToken());
        }

        public async Task<bool> IsTokenValidAsync(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return true;
            }

            return await this.sessionTokenRepository.ExistsAsync(token);
        }

        public async Task InvalidateTokenAsync(string token)
        {
            await this.sessionTokenRepository.DeleteTokenAsync(token);
        }

        #endregion

        #region Private Methods

        private string GetCurrentToken()
        {
            var authorizationHeader = this.httpContextAccessor.HttpContext.Request.Headers["authorization"];

            return authorizationHeader == StringValues.Empty ? string.Empty : authorizationHeader.Single().Split(" ").Last();
        }

        #endregion
    }
}

