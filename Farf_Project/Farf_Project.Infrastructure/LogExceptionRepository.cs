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

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task StoreLogExceptionAsync(Guid exceptionId, string exception)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
#pragma warning disable CS0219 // The variable 'sql' is assigned but its value is never used
            var sql = "INSERT INTO LogException (id, exception) VALUES (@exceptionId, @exception)";
#pragma warning restore CS0219 // The variable 'sql' is assigned but its value is never used
            var data = new
            {
                exceptionId,
                exception
            };
            //await this.dbConnection.ExecuteAsync(sql, data);

        }
      
    }
}
