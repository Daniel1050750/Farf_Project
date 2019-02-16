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

        private static readonly string GET_POINTS_SQL = @"SELECT * FROM Point WHERE IsDeleted = FALSE";
        
        private static readonly string GET_POINT_SQL = @"SELECT u.*, r.*
                                                            FROM ""Point"" u
                                                            INNER JOIN Role AS r ON r.Id = u.RoleId
                                                         WHERE u.id = @id AND u.IsDeleted = FALSE";

        private static readonly string GET_USER_BY_USERNAME_SQL = @"SELECT * FROM ""Point""
                                                                    WHERE pointname = @pointname AND IsDeleted = FALSE";

        private static readonly string CREATE_USER_SQL = @"INSERT INTO ""Point"" (Id, Name, Pointname, RoleId, State, Password, PasswordSalt)
                                                           VALUES(@Id, @Name, @Pointname, @RoleId, @State, @Password, @PasswordSalt)";

        private static readonly string DELETE_POINT_SQL = @"UPDATE Point SET IsDeleted = TRUE WHERE u.Id = @Id";

        private static readonly string UPDATE_POINT_SQL = @"UPDATE Point SET Name = @Name, Pointname = @Pointname, RoleId = @RoleId, State = @State
                                                                       WHERE u.Id = @Id and IsDeleted = FALSE";

        #endregion SQL

        private readonly IDbConnection dbConnection;

        public PointsRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Point>> GetPointsAsync()
        {            
            var lookup = new Dictionary<Guid, Point>();
            await this.dbConnection.QueryAsync<Point>(GET_POINTS_SQL);
            var points = lookup.Select(x => x.Value).ToList();
            return points;
        }

        public async Task<Point> GetPointAsync(Guid id)
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

        public async Task DeletePointAsync(Guid id)
        {
            await this.dbConnection.ExecuteAsync(DELETE_POINT_SQL, new { Id = id });
        }

        public async Task UpdatePointAsync(Point point)
        {
            var newPointAndPassword = new
            {
                point.Id,
                point.Name,
                point.State
            };

            await this.dbConnection.ExecuteAsync(UPDATE_POINT_SQL, newPointAndPassword);
        }
    }
}