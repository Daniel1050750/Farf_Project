using Dapper;
using System;
using System.Data;
using System.Threading.Tasks;
using Farf_Project.Core;

namespace Farf_Project.Infrastructure
{
    public class LogExceptionRepository : ILogExceptionRepository
    {
        private readonly IDbConnection dbConnection;

        public LogExceptionRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
            this.dbConnection.Open();
        }

        /// <summary>
        /// Store exception on DB
        /// </summary>
        /// <param name="exceptionId"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        public async Task StoreLogExceptionAsync(Guid exceptionId, string exception)
        {
            var sql = "INSERT INTO LogException (id, exception) VALUES (@exceptionId, @exception)";
            var data = new
            {
                exceptionId,
                exception
            };
            await this.dbConnection.ExecuteAsync(sql, data);
        }
    }
}
