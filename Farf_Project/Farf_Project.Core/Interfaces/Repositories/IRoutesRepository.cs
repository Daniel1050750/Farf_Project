﻿using System;
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
        Task CreateRouteAsync(Route route);

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
        /// Get point on active route
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Route</returns>
        Task<Route> GetPointOnRoute(Guid id);

        /// <summary>
        /// Gets the routes with start point with ID asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Route>> GetRoutesWithStartPoint(Guid id);

        /// <summary>
        /// Gets the routes with end point with ID asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Route>> GetRoutesWithEndPoint(Guid id);
    }
}
