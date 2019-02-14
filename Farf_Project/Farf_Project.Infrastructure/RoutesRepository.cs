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

#pragma warning disable CS0414 // The field 'RoutesRepository.GET_USERS_SQL' is assigned but its value is never used
        private static readonly string GET_USERS_SQL = @"SELECT u.*, r.*
                                                            FROM ""Route"" u
                                                            INNER JOIN Role AS r ON r.Id = u.RoleId
                                                         WHERE u.IsDeleted = FALSE"
#pragma warning restore CS0414 // The field 'RoutesRepository.GET_USERS_SQL' is assigned but its value is never used
;

#pragma warning disable CS0414 // The field 'RoutesRepository.GET_USER_SQL' is assigned but its value is never used
        private static readonly string GET_USER_SQL = @"SELECT u.*, r.*
                                                            FROM ""Route"" u
                                                            INNER JOIN Role AS r ON r.Id = u.RoleId
                                                         WHERE u.id = @id AND u.IsDeleted = FALSE"
#pragma warning restore CS0414 // The field 'RoutesRepository.GET_USER_SQL' is assigned but its value is never used
;

        private static readonly string GET_USER_BY_USERNAME_SQL = @"SELECT * FROM ""Route""
                                                                    WHERE routename = @routename AND IsDeleted = FALSE";

        private static readonly string CREATE_USER_SQL = @"INSERT INTO ""Route"" (Id, Name, Routename, RoleId, State, Password, PasswordSalt)
                                                           VALUES(@Id, @Name, @Routename, @RoleId, @State, @Password, @PasswordSalt)";

        private static readonly string DELETE_USER_SQL = @"UPDATE ""Route"" u SET IsDeleted = TRUE
                                                            WHERE u.Id = @Id";

        private static readonly string GET_PASSWORD_SALT_FROM_USER_SQL = @" SELECT PasswordSalt FROM ""Route""
                                                                            WHERE id = @id AND IsDeleted = FALSE";

        private static readonly string VERIFY_PASSWORD_FROM_USER_SQL = @"SELECT COUNT(*) from ""Route""
                                                                         WHERE id = @id AND password = @password AND IsDeleted = FALSE";

        private static readonly string UPDATE_USER_SQL = @"UPDATE ""Route"" u SET Name = @Name, Routename = @Routename, RoleId = @RoleId, State = @State
                                                                       WHERE u.Id = @Id and IsDeleted = FALSE";

        private static readonly string UPDATE_USERPASSWORD_SQL = @"UPDATE ""Route"" u SET Password = @Password, PasswordSalt = @PasswordSalt
                                                                       WHERE u.Id = @Id and u.IsDeleted = FALSE";

        #endregion SQL

        private readonly IDbConnection dbConnection;

        public RoutesRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<IEnumerable<Route>> GetRoutesAsync()
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {            
            var lookup = new Dictionary<Guid, Route>();
            //await this.dbConnection.QueryAsync<Route>(
            //    GET_USERS_SQL,
            //    (u, r) =>
            //    {
            //        if (!lookup.TryGetValue(u.Id, out Route route))
            //        {
            //            lookup.Add(u.Id, route = u);
            //        }

            //        if (r.Id != Guid.Empty)
            //        {
            //            route.Role = r;
            //        }

            //        return route;
            //    },
            //    splitOn: "RoleId"
            //);

            var routes = lookup.Select(x => x.Value).ToList();

            return routes;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<Route> GetRouteAsync(Guid id)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Route route = null;
            //await this.dbConnection.QueryAsync<Route>(
            //    GET_USER_SQL,
            //    (u, r) =>
            //    {
            //        if (route == null)
            //        {
            //            route = u;
            //        }

            //        if (r.Id != Guid.Empty)
            //        {
            //            route.Role = r;
            //        }

            //        return route;
            //    },
            //    splitOn: "RoleId",
            //    param: new { Id = id }
            //);

            return route;
        }

        public async Task CreateRouteAsync(Route route, string password, string passwordSalt)
        {
            var newRoute = new
            {
                route.Id,
                route.Name,
                Password = password,
                PasswordSalt = passwordSalt
            };

            await this.dbConnection.ExecuteAsync(CREATE_USER_SQL, newRoute);
        }

        public async Task<Route> GetRouteByRoutenameAsync(string routename)
        {
            var route = await this.dbConnection.QueryFirstOrDefaultAsync<Route>(GET_USER_BY_USERNAME_SQL, new { routename = routename.ToLowerInvariant() });

            return route;
        }

        public async Task<string> GetPasswordSaltAsync(Guid routeId)
        {
            var salt = await this.dbConnection.QueryFirstOrDefaultAsync<string>(GET_PASSWORD_SALT_FROM_USER_SQL, new { id = routeId });

            return salt;
        }

        public async Task<bool> VerifyPasswordAsync(Guid routeId, string password)
        {
            var validPassword = await this.dbConnection.QueryFirstAsync<bool>(VERIFY_PASSWORD_FROM_USER_SQL, new { id = routeId, password });

            return validPassword;
        }

        public async Task DeleteRouteAsync(Guid id)
        {
            await this.dbConnection.ExecuteAsync(DELETE_USER_SQL, new { Id = id });
        }

        public async Task UpdateRouteAsync(Route route)
        {
            var newRouteAndPassword = new
            {
                route.Id,
                route.Name
            };

            await this.dbConnection.ExecuteAsync(UPDATE_USER_SQL, newRouteAndPassword);
        }

        public async Task UpdateRoutePasswordAsync(Guid id, string password, string saltpassword)
        {
            var newRoute = new
            {
                id,
                Password = password,
                PasswordSalt = saltpassword
            };
            await this.dbConnection.ExecuteAsync(UPDATE_USERPASSWORD_SQL, newRoute);
        }
    }
}