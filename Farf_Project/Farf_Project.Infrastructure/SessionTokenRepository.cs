using Dapper;
using System;
using System.Data;
using System.Threading.Tasks;
using Farf_Project.Core;

namespace Farf_Project.Infrastructure
{

    public class SessionTokenRepository: ISessionTokenRepository
    {
        private readonly IDbConnection dbConnection;

        public SessionTokenRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
            this.dbConnection.Open();
        }

        public async Task SaveTokenAsync(string token, DateTime expirationDate, User user)
        {
            var sql = "INSERT INTO SessionToken (Token, ExpirationDate, UserId) VALUES (@token, @expirationDate, @userId)";
            var data = new
            {
                token,
                expirationDate,
                userId = user.Id
            };

            await this.dbConnection.ExecuteAsync(sql, data);
        }

        public async Task DeleteTokenAsync(string token)
        {
            var sql = "DELETE FROM SessionToken WHERE Token = @token";
            var data = new { token };

            await this.dbConnection.ExecuteAsync(sql, data);
        }

        public async Task DeleteAllTokensFromUserAsync(User user)
        {
            var sql = "DELETE FROM SessionToken WHERE UserId = @userId";
            var data = new { userId = user.Id };

            await this.dbConnection.ExecuteAsync(sql, data);
        }

        public async Task<bool> ExistsAsync(string token)
        {
            var sql = "SELECT EXISTS (SELECT 1 FROM SessionToken WHERE Token = @token)";
            var data = new { token };

            return await this.dbConnection.QueryFirstAsync<bool>(sql, data);
        }
    }
}
