using System;
using System.Threading.Tasks;

namespace Farf_Project.Core
{
    public interface ISessionTokenRepository
    {
        Task SaveTokenAsync(string token, DateTime expirationDate, User user);
        Task DeleteTokenAsync(string token);
        Task DeleteAllTokensFromUserAsync(User user);
        Task<bool> ExistsAsync(string token);
    }
}