using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Farf_Project.Core
{
    /// <summary>
    /// Repository to access, create and update Routes on database.
    /// </summary>
    public interface IRoutesRepository
    {
        /// <summary>
        /// Creates the route asynchronous.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <param name="password">The password.</param>
        /// <param name="passwordSalt">The password salt.</param>
        /// <returns></returns>
        Task CreateRouteAsync(Route route, string password, string passwordSalt);

        /// <summary>
        /// Gets the routes asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Route>> GetRoutesAsync();

        /// <summary>
        /// Gets the route asynchronous.
        /// </summary>
        /// <param name="id">The route id.</param>
        /// <returns>
        /// The route that matchs the id.
        /// </returns>
        Task<Route> GetRouteAsync(Guid id);

        /// <summary>
        /// Gets the route by routename asynchronous.
        /// </summary>
        /// <param name="routename">The routename.</param>
        /// <returns>
        /// The route that matchs the routename.
        /// </returns>
        Task<Route> GetRouteByRoutenameAsync(string routename);

        /// <summary>
        /// Gets the password salt asynchronous.
        /// </summary>
        /// <param name="routeId">The route identifier.</param>
        /// <returns>The password salt.</returns>
        Task<string> GetPasswordSaltAsync(Guid routeId);

        /// <summary>
        /// Verifies the password asynchronous.
        /// </summary>
        /// <param name="routeId">The route identifier.</param>
        /// <param name="password">The password.</param>
        /// <returns>True if the password is valid for the provided route, false otherwise.</returns>
        Task<bool> VerifyPasswordAsync(Guid routeId, string password);

        /// <summary>
        /// Deletes the route asynchronously.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteRouteAsync(Guid id);

        /// <summary>
        /// Update route data
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
        Task UpdateRouteAsync(Route route);

        /// <summary>
        /// Update route password
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
        Task UpdateRoutePasswordAsync(Guid id, string securePassword, string salt);
    }
}
