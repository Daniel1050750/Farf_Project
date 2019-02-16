using Farf_Project.Core;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Farf_Project.Infrastructure
{

    public class UsersRepository : IUsersRepository
    {
        #region SQL

        private static readonly string GET_USERS_SQL = @"SELECT * FROM ""User"" WHERE IsDeleted = FALSE";

        private static readonly string GET_USER_SQL = @"SELECT * FROM ""User"" WHERE id = @id AND IsDeleted = FALSE";

        private static readonly string GET_USER_BY_USERNAME_SQL = @"SELECT * FROM ""User"" WHERE username = @username AND IsDeleted = FALSE";

        private static readonly string CREATE_USER_SQL = @"INSERT INTO ""User"" (Id, Username, Role, State, Password, PasswordSalt)
                                                           VALUES(@Id, @Username, @Role, @State, @Password, @PasswordSalt)";

        private static readonly string DELETE_USER_SQL = @"UPDATE ""User"" SET IsDeleted = TRUE WHERE Id = @Id";

        private static readonly string GET_PASSWORD_SALT_FROM_USER_SQL = @"SELECT PasswordSalt FROM ""User"" WHERE id = @id AND IsDeleted = FALSE";

        private static readonly string VERIFY_PASSWORD_FROM_USER_SQL = @"SELECT COUNT(*) from ""User""
                                                                         WHERE id = @id AND password = @password AND IsDeleted = FALSE";

        private static readonly string UPDATE_USER_SQL = @"UPDATE ""User"" SET Username = @Username, Role = @Role, State = @State
                                                                       WHERE Id = @Id and IsDeleted = FALSE";

        private static readonly string UPDATE_USERPASSWORD_SQL = @"UPDATE ""User"" SET Password = @Password, PasswordSalt = @PasswordSalt
                                                                       WHERE Id = @Id and IsDeleted = FALSE";

        #endregion SQL

        private readonly IDbConnection dbConnection;

        public UsersRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        /// <summary>
        /// Get all active users
        /// </summary>
        /// <returns>Users List</returns>
        public async Task<IEnumerable<User>> GetUsersAsync()
        {           
            var users = await this.dbConnection.QueryAsync<User>(GET_USERS_SQL);
            return users;
        }

        /// <summary>
        /// Get user by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User</returns>
        public async Task<User> GetUserAsync(Guid id)
        {
            var user = await this.dbConnection.QueryFirstOrDefaultAsync<User>(GET_USER_SQL, new { Id = id });
            return user;
        }

        /// <summary>
        /// Store de user data into DB
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="passwordSalt"></param>
        public async Task CreateUserAsync(User user, string password, string passwordSalt)
        {
            var newUser = new
            {
                user.Id,
                Username = user.Username?.ToLowerInvariant(),
                user.State,
                user.Role,
                Password = password,
                PasswordSalt = passwordSalt
            };
            await this.dbConnection.ExecuteAsync(CREATE_USER_SQL, newUser);
        }

        /// <summary>
        /// Get all users by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns>User</returns>
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            var user = await this.dbConnection.QueryFirstOrDefaultAsync<User>(GET_USER_BY_USERNAME_SQL, new { username = username.ToLowerInvariant() });
            return user;
        }

        /// <summary>
        /// Get passwordsalt by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>PasswordSalt</returns>
        public async Task<string> GetPasswordSaltAsync(Guid userId)
        {
            var salt = await this.dbConnection.QueryFirstOrDefaultAsync<string>(GET_PASSWORD_SALT_FROM_USER_SQL, new { id = userId });
            return salt;
        }

        /// <summary>
        /// User credentials validation
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns>True or False</returns>
        public async Task<bool> VerifyPasswordAsync(Guid userId, string password)
        {
            var validPassword = await this.dbConnection.QueryFirstAsync<bool>(VERIFY_PASSWORD_FROM_USER_SQL, new { id = userId, password });
            return validPassword;
        }

        /// <summary>
        /// Update isDeleted value for true for this user
        /// </summary>
        /// <param name="id"></param>
        public async Task DeleteUserAsync(Guid id)
        {
            await this.dbConnection.ExecuteAsync(DELETE_USER_SQL, new { Id = id });
        }

        /// <summary>
        /// Update user data on DB
        /// </summary>
        /// <param name="user"></param>
        public async Task UpdateUserAsync(User user)
        {
            var newUserAndPassword = new
            {
                user.Id,
                Username = user.Username?.ToLowerInvariant(),
                user.State,
                user.Role
            };
            await this.dbConnection.ExecuteAsync(UPDATE_USER_SQL, newUserAndPassword);
        }

        /// <summary>
        /// Update user password
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <param name="saltpassword"></param>
        /// <returns></returns>
        public async Task UpdateUserPasswordAsync(Guid id, string password, string saltpassword)
        {
            var newUser = new
            {
                id,
                Password = password,
                PasswordSalt = saltpassword
            };
            await this.dbConnection.ExecuteAsync(UPDATE_USERPASSWORD_SQL, newUser);
        }
    }
}