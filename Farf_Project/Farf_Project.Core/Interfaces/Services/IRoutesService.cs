﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Farf_Project.Core
{
    public interface IRoutesService
    {
        /// <summary>
        /// Get all routes asynchronous.
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>
        Task<Route> GetRoutes(Route route);

        /// <summary>
        /// Create a new route asynchronous.
        /// </summary>
        /// <param name="route"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task CreateRouteAsync(Route route, string password);

        /// <summary>
        /// Delete a route by ID asynchronous.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteRouteAsync(Guid id);

        /// <summary>
        ///  
        /// </summary>
        /// <returns></returns>
        Task<IList<Route>> GetRoutesListAsync();

        /// <summary>
        /// Get route by routename
        /// </summary>
        /// <param name="routename"></param>
        /// <returns></returns>
        Task GetRouteAsync(string routename);
        /// <summary>
        /// Gets the route by routename asynchronous.
        /// </summary>
        /// <param name="routename">The routename.</param>
        /// <returns>The route.</returns>
        Task<Route> GetRouteByRoutenameAsync(string routename);

        /// <summary>
        /// Gets the route asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The route.</returns>
        Task<Route> GetRouteAsync(Guid id);

        /// <summary>
        /// Validates the credentials.
        /// </summary>
        /// <param name="routename">The routename.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        Task ValidateCredentialsAsync(string routename, string password);

        /// <summary>
        /// Update route data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="route"></param>
        /// <param name="password"></param>
        /// <param name="repassword"></param>
        /// <returns></returns>
        Task UpdateRouteAsync(Route route, string password);
    }
}