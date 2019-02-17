using Farf_Project.Core;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Farf_Project.Infrastructure
{

    public class RoutesRepository : IRoutesRepository
    {
        #region SQL

        private static readonly string GET_ROUTES_SQL = @"SELECT * FROM ""Route"" WHERE IsDeleted = FALSE";
        
        private static readonly string GET_ROUTE_SQL = @"SELECT * FROM ""Route"" WHERE id = @id AND IsDeleted = FALSE";

        private static readonly string GET_POINT_ON_ROUTE_SQL = @"SELECT * FROM ""Route"" WHERE (PointStart = @id OR PointEnd = @id) AND IsDeleted = FALSE";

        private static readonly string GET_SP_ROUTES_SQL = @"SELECT * FROM ""Route"" WHERE PointStart = @PointStart AND IsDeleted = FALSE";

        private static readonly string GET_EP_ROUTES_SQL = @"SELECT * FROM ""Route"" WHERE PointEnd = @PointEnd AND IsDeleted = FALSE";

        private static readonly string GET_ROUTE_BY_NAME_SQL = @"SELECT * FROM ""Route"" WHERE Name = @Name AND IsDeleted = FALSE";

        private static readonly string CREATE_ROUTE_SQL = @"INSERT INTO ""Route"" (Id, Name, PointStart, PointEnd, RoutePrice, RouteTime, State)
                                                           VALUES(@Id, @Name, @PointStart, @PointEnd, @RoutePrice, @RouteTime, @State)";

        private static readonly string DELETE_ROUTE_SQL = @"UPDATE ""Route"" SET IsDeleted = TRUE WHERE Id = @Id";

        private static readonly string UPDATE_ROUTE_SQL = @"UPDATE ""Route""
                                                                    SET Name = @Name, PointStart = @PointStart, PointEnd = @PointEnd, 
                                                                    RoutePrice = @RoutePrice, RouteTime = @RouteTime, State = @State
                                                                       WHERE Id = @Id and IsDeleted = FALSE";

        #endregion SQL

        private readonly IDbConnection dbConnection;

        public RoutesRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        /// <summary>
        /// Get all active routes
        /// </summary>
        /// <returns>Routes list</returns>
        public async Task<IEnumerable<Route>> GetRoutesAsync()
        {            
            var routes = await this.dbConnection.QueryAsync<Route>(GET_ROUTES_SQL);
            return routes;
        }

        /// <summary>
        /// Get route by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Route</returns>
        public async Task<Route> GetRouteAsync(Guid id)
        {
            var route = await this.dbConnection.QueryFirstOrDefaultAsync<Route>(GET_ROUTE_SQL, new { ID = id });
            return route;
        }

        /// <summary>
        /// Create new route
        /// </summary>
        /// <param name="route"></param>
        public async Task CreateRouteAsync(Route route)
        {
            var newRoute = new
            {
                route.Id,
                Name = route.Name.ToLowerInvariant(),
                route.PointStart,
                route.PointEnd,
                route.RoutePrice,
                route.RouteTime,
                route.State
            };
            await this.dbConnection.ExecuteAsync(CREATE_ROUTE_SQL, newRoute);
        }

        /// <summary>
        /// Get route by name
        /// </summary>
        /// <param name="routename"></param>
        /// <returns>Route</returns>
        public async Task<Route> GetRouteByRoutenameAsync(string routename)
        {
            var route = await this.dbConnection.QueryFirstOrDefaultAsync<Route>(GET_ROUTE_BY_NAME_SQL, new { Name = routename.ToLowerInvariant() });
            return route;
        }

        /// <summary>
        /// Delete route
        /// </summary>
        /// <param name="id"></param>
        public async Task DeleteRouteAsync(Guid id)
        {
            await this.dbConnection.ExecuteAsync(DELETE_ROUTE_SQL, new { Id = id });
        }

        /// <summary>
        /// Update route data
        /// </summary>
        /// <param name="route"></param>
        public async Task UpdateRouteAsync(Route route)
        {
            var updateRoute = new
            {
                route.Id,
                Name = route.Name.ToLowerInvariant(),
                route.PointStart,
                route.PointEnd,
                route.RoutePrice,
                route.RouteTime,
                route.State
            };
            await this.dbConnection.ExecuteAsync(UPDATE_ROUTE_SQL, updateRoute);
        }

        /// <summary>
        /// Get point on active route
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Route</returns>
        public async Task<Route> GetPointOnRoute(Guid id)
        {
            var route = await this.dbConnection.QueryFirstOrDefaultAsync<Route>(GET_POINT_ON_ROUTE_SQL, new { ID = id });
            return route;
        }

        /// <summary>
        /// Find all routes with this ID start point
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Route>> GetRoutesWithStartPoint(Guid id)
        {
            var routes = await this.dbConnection.QueryAsync<Route>(GET_SP_ROUTES_SQL, new { PointStart = id });
            return routes;
        }

        /// <summary>
        /// Find all routes with this ID end point
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Route>> GetRoutesWithEndPoint(Guid id)
        {
            var routes = await this.dbConnection.QueryAsync<Route>(GET_EP_ROUTES_SQL, new { PointEnd = id });
            return routes;
        }
    }
}