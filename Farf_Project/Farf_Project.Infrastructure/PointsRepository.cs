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
        
        private static readonly string GET_POINT_SQL = @"SELECT * FROM Point WHERE id = @id AND IsDeleted = FALSE";

        private static readonly string GET_POINT_BY_NAME_SQL = @"SELECT * FROM Point WHERE Name = @Name AND IsDeleted = FALSE";

        private static readonly string CREATE_POINT_SQL = @"INSERT INTO Point (Id, Name, Address, State) VALUES(@Id, @Name, @Address, @State)";

        private static readonly string DELETE_POINT_SQL = @"UPDATE Point SET IsDeleted = TRUE WHERE Id = @Id";

        private static readonly string UPDATE_POINT_SQL = @"UPDATE Point SET Name = @Name, Address = @Address, State = @State WHERE Id = @Id and IsDeleted = FALSE";

        #endregion SQL

        private readonly IDbConnection dbConnection;

        public PointsRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        /// <summary>
        /// Get all active points
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Point>> GetPointsAsync()
        {            
            var points = await this.dbConnection.QueryAsync<Point>(GET_POINTS_SQL);
            return points;
        }

        /// <summary>
        /// Get point by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Point> GetPointAsync(Guid id)
        {
            var point = await this.dbConnection.QueryFirstOrDefaultAsync<Point>(GET_POINT_SQL, new { Id = id });
            return point;
        }

        /// <summary>
        /// Create new point
        /// </summary>
        /// <param name="point"></param>
        public async Task CreatePointAsync(Point point)
        {
            var newpoint = new
            {
                point.Id,
                Name = point.Name.ToLowerInvariant(),
                point.Address,
                point.State
            };
            await this.dbConnection.ExecuteAsync(CREATE_POINT_SQL, newpoint);
        }

        /// <summary>
        /// Get point by name
        /// </summary>
        /// <param name="pointname"></param>
        /// <returns>Point</returns>
        public async Task<Point> GetPointByPointnameAsync(string pointname)
        {
            var point = await this.dbConnection.QueryFirstOrDefaultAsync<Point>(GET_POINT_BY_NAME_SQL, new { Name = pointname.ToLowerInvariant()});
            return point;
        }

        /// <summary>
        /// Update isDeleted to TRUE
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeletePointAsync(Guid id)
        {
            await this.dbConnection.ExecuteAsync(DELETE_POINT_SQL, new { Id = id });
        }

        /// <summary>
        /// Update piont data
        /// </summary>
        /// <param name="point"></param>
        public async Task UpdatePointAsync(Point point)
        {
            var updatePoint = new
            {
                point.Id,
                Name = point.Name.ToLowerInvariant(),
                point.Address,
                point.State
            };
            await this.dbConnection.ExecuteAsync(UPDATE_POINT_SQL, updatePoint);
        }
    }
}