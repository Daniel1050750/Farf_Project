using System;
using System.Threading.Tasks;

namespace Farf_Project.Core
{
    public interface ITokenManager
    {
        /// <summary>
        /// Token validation
        /// </summary>
        /// <returns>True or False</returns>
        Task<bool> IsCurrentTokenValidAsync();

        /// <summary>
        /// Token invalidation
        /// </summary>
        Task InvalidateCurrentTokenAsync();

        /// <summary>
        /// Check token validation
        /// </summary>
        /// <param name="token"></param>
        /// <returns>True or False</returns>
        Task<bool> IsTokenValidAsync(string token);

        /// <summary>
        /// Invalid token
        /// </summary>
        /// <param name="token"></param>
        Task InvalidateTokenAsync(string token);

        /// <summary>
        /// Store token
        /// </summary>
        /// <param name="token"></param>
        /// <param name="expirationDate"></param>
        /// <param name="user"></param>
        Task StoreTokenAsync(string token, DateTime expirationDate, User user);
    }
}

