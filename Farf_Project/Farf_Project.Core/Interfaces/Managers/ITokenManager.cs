using System;
using System.Threading.Tasks;

namespace Farf_Project.Core
{
    public interface ITokenManager
    {
        Task<bool> IsCurrentTokenValidAsync();
        Task InvalidateCurrentTokenAsync();
        Task<bool> IsTokenValidAsync(string token);
        Task InvalidateTokenAsync(string token);
        Task StoreTokenAsync(string token, DateTime expirationDate, User user);
    }
}

