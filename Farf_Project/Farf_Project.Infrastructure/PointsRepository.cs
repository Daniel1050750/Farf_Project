using Farf_Project.Core;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Farf_Project.Infrastructure
{

    public class PointsRepository : IPointsRepository
    {
        #region SQL

#pragma warning disable CS0414 // The field 'PointsRepository.GET_USERS_SQL' is assigned but its value is never used
        private static readonly string GET_USERS_SQL = @"SELECT u.*, r.*
                                                            FROM ""Point"" u
                                                            INNER JOIN Role AS r ON r.Id = u.RoleId
                                                         WHERE u.IsDeleted = FALSE"
#pragma warning restore CS0414 // The field 'PointsRepository.GET_USERS_SQL' is assigned but its value is never used
;

#pragma warning disable CS0414 // The field 'PointsRepository.GET_USER_SQL' is assigned but its value is never used
        private static readonly string GET_USER_SQL = @"SELECT u.*, r.*
                                                            FROM ""Point"" u
                                                            INNER JOIN Role AS r ON r.Id = u.RoleId
                                                         WHERE u.id = @id AND u.IsDeleted = FALSE"
#pragma warning restore CS0414 // The field 'PointsRepository.GET_USER_SQL' is assigned but its value is never used
;

        private static readonly string GET_USER_BY_USERNAME_SQL = @"SELECT * FROM ""Point""
                                                                    WHERE pointname = @pointname AND IsDeleted = FALSE";

        private static readonly string CREATE_USER_SQL = @"INSERT INTO ""Point"" (Id, Name, Pointname, RoleId, State, Password, PasswordSalt)
                                                           VALUES(@Id, @Name, @Pointname, @RoleId, @State, @Password, @PasswordSalt)";

        private static readonly string DELETE_USER_SQL = @"UPDATE ""Point"" u SET IsDeleted = TRUE
                                                            WHERE u.Id = @Id";

        private static readonly string GET_PASSWORD_SALT_FROM_USER_SQL = @" SELECT PasswordSalt FROM ""Point""
                                                                            WHERE id = @id AND IsDeleted = FALSE";

        private static readonly string VERIFY_PASSWORD_FROM_USER_SQL = @"SELECT COUNT(*) from ""Point""
                                                                         WHERE id = @id AND password = @password AND IsDeleted = FALSE";

        private static readonly string UPDATE_USER_SQL = @"UPDATE ""Point"" u SET Name = @Name, Pointname = @Pointname, RoleId = @RoleId, State = @State
                                                                       WHERE u.Id = @Id and IsDeleted = FALSE";

        private static readonly string UPDATE_USERPASSWORD_SQL = @"UPDATE ""Point"" u SET Password = @Password, PasswordSalt = @PasswordSalt
                                                                       WHERE u.Id = @Id and u.IsDeleted = FALSE";

        #endregion SQL

        private readonly IDbConnection dbConnection;

        public PointsRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<IEnumerable<Point>> GetPointsAsync()
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {            
            var lookup = new Dictionary<Guid, Point>();
            //await this.dbConnection.QueryAsync<Point>(
            //    GET_USERS_SQL,
            //    (u, r) =>
            //    {
            //        if (!lookup.TryGetValue(u.Id, out Point point))
            //        {
            //            lookup.Add(u.Id, point = u);
            //        }

            //        if (r.Id != Guid.Empty)
            //        {
            //            point.Role = r;
            //        }

            //        return point;
            //    },
            //    splitOn: "RoleId"
            //);

            var points = lookup.Select(x => x.Value).ToList();

            return points;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<Point> GetPointAsync(Guid id)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Point point = null;
            //await this.dbConnection.QueryAsync<Point>(
            //    GET_USER_SQL,
            //    (u, r) =>
            //    {
            //        if (point == null)
            //        {
            //            point = u;
            //        }

            //        if (r.Id != Guid.Empty)
            //        {
            //            point.Role = r;
            //        }

            //        return point;
            //    },
            //    splitOn: "RoleId",
            //    param: new { Id = id }
            //);

            return point;
        }

        public async Task CreatePointAsync(Point point)
        {
            var newPoint = new
            {
                point.Id,
                point.Name,
                point.State
            };

            await this.dbConnection.ExecuteAsync(CREATE_USER_SQL, newPoint);
        }

        public async Task<Point> GetPointByPointnameAsync(string pointname)
        {
            var point = await this.dbConnection.QueryFirstOrDefaultAsync<Point>(GET_USER_BY_USERNAME_SQL, new { pointname = pointname.ToLowerInvariant() });

            return point;
        }

        public async Task<string> GetPasswordSaltAsync(Guid pointId)
        {
            var salt = await this.dbConnection.QueryFirstOrDefaultAsync<string>(GET_PASSWORD_SALT_FROM_USER_SQL, new { id = pointId });

            return salt;
        }

        public async Task<bool> VerifyPasswordAsync(Guid pointId, string password)
        {
            var validPassword = await this.dbConnection.QueryFirstAsync<bool>(VERIFY_PASSWORD_FROM_USER_SQL, new { id = pointId, password });

            return validPassword;
        }

        public async Task DeletePointAsync(Guid id)
        {
            await this.dbConnection.ExecuteAsync(DELETE_USER_SQL, new { Id = id });
        }

        public async Task UpdatePointAsync(Point point)
        {
            var newPointAndPassword = new
            {
                point.Id,
                point.Name,
                point.State
            };

            await this.dbConnection.ExecuteAsync(UPDATE_USER_SQL, newPointAndPassword);
        }

        public async Task UpdatePointPasswordAsync(Guid id, string password, string saltpassword)
        {
            var newPoint = new
            {
                id,
                Password = password,
                PasswordSalt = saltpassword
            };
            await this.dbConnection.ExecuteAsync(UPDATE_USERPASSWORD_SQL, newPoint);
        }
    }
}