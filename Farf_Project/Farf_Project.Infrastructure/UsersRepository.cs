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

#pragma warning disable CS0414 // The field 'UsersRepository.GET_USERS_SQL' is assigned but its value is never used
        private static readonly string GET_USERS_SQL = @"SELECT u.*, r.*
                                                            FROM ""User"" u
                                                            INNER JOIN Role AS r ON r.Id = u.RoleId
                                                         WHERE u.IsDeleted = FALSE"
#pragma warning restore CS0414 // The field 'UsersRepository.GET_USERS_SQL' is assigned but its value is never used
;

#pragma warning disable CS0414 // The field 'UsersRepository.GET_USER_SQL' is assigned but its value is never used
        private static readonly string GET_USER_SQL = @"SELECT u.*, r.*
                                                            FROM ""User"" u
                                                            INNER JOIN Role AS r ON r.Id = u.RoleId
                                                         WHERE u.id = @id AND u.IsDeleted = FALSE"
#pragma warning restore CS0414 // The field 'UsersRepository.GET_USER_SQL' is assigned but its value is never used
;

        private static readonly string GET_USER_BY_USERNAME_SQL = @"SELECT * FROM ""User""
                                                                    WHERE username = @username AND IsDeleted = FALSE";

        private static readonly string CREATE_USER_SQL = @"INSERT INTO ""User"" (Id, Name, Username, RoleId, State, Password, PasswordSalt)
                                                           VALUES(@Id, @Name, @Username, @RoleId, @State, @Password, @PasswordSalt)";

        private static readonly string DELETE_USER_SQL = @"UPDATE ""User"" u SET IsDeleted = TRUE
                                                            WHERE u.Id = @Id";

        private static readonly string GET_PASSWORD_SALT_FROM_USER_SQL = @" SELECT PasswordSalt FROM ""User""
                                                                            WHERE id = @id AND IsDeleted = FALSE";

        private static readonly string VERIFY_PASSWORD_FROM_USER_SQL = @"SELECT COUNT(*) from ""User""
                                                                         WHERE id = @id AND password = @password AND IsDeleted = FALSE";

        private static readonly string UPDATE_USER_SQL = @"UPDATE ""User"" u SET Name = @Name, Username = @Username, RoleId = @RoleId, State = @State
                                                                       WHERE u.Id = @Id and IsDeleted = FALSE";

        private static readonly string UPDATE_USERPASSWORD_SQL = @"UPDATE ""User"" u SET Password = @Password, PasswordSalt = @PasswordSalt
                                                                       WHERE u.Id = @Id and u.IsDeleted = FALSE";

        #endregion SQL

        private readonly IDbConnection dbConnection;

        public UsersRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<IEnumerable<User>> GetUsersAsync()
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {            
            var lookup = new Dictionary<Guid, User>();
            //await this.dbConnection.QueryAsync<User>(
            //    GET_USERS_SQL,
            //    (u, r) =>
            //    {
            //        if (!lookup.TryGetValue(u.Id, out User user))
            //        {
            //            lookup.Add(u.Id, user = u);
            //        }

            //        if (r.Id != Guid.Empty)
            //        {
            //            user.Role = r;
            //        }

            //        return user;
            //    },
            //    splitOn: "RoleId"
            //);

            var users = lookup.Select(x => x.Value).ToList();

            return users;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<User> GetUserAsync(Guid id)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            User user = null;
            //await this.dbConnection.QueryAsync<User>(
            //    GET_USER_SQL,
            //    (u, r) =>
            //    {
            //        if (user == null)
            //        {
            //            user = u;
            //        }

            //        if (r.Id != Guid.Empty)
            //        {
            //            user.Role = r;
            //        }

            //        return user;
            //    },
            //    splitOn: "RoleId",
            //    param: new { Id = id }
            //);

            return user;
        }

        public async Task CreateUserAsync(User user, string password, string passwordSalt)
        {
            var newUser = new
            {
                user.Id,
                user.Name,
                Username = user.Username?.ToLowerInvariant(),
                user.State,
                Password = password,
                PasswordSalt = passwordSalt
            };

            await this.dbConnection.ExecuteAsync(CREATE_USER_SQL, newUser);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            var user = await this.dbConnection.QueryFirstOrDefaultAsync<User>(GET_USER_BY_USERNAME_SQL, new { username = username.ToLowerInvariant() });

            return user;
        }

        public async Task<string> GetPasswordSaltAsync(Guid userId)
        {
            var salt = await this.dbConnection.QueryFirstOrDefaultAsync<string>(GET_PASSWORD_SALT_FROM_USER_SQL, new { id = userId });

            return salt;
        }

        public async Task<bool> VerifyPasswordAsync(Guid userId, string password)
        {
            var validPassword = await this.dbConnection.QueryFirstAsync<bool>(VERIFY_PASSWORD_FROM_USER_SQL, new { id = userId, password });

            return validPassword;
        }

        public async Task DeleteUserAsync(Guid id)
        {
            await this.dbConnection.ExecuteAsync(DELETE_USER_SQL, new { Id = id });
        }

        public async Task UpdateUserAsync(User user)
        {
            var newUserAndPassword = new
            {
                user.Id,
                user.Name,
                Username = user.Username?.ToLowerInvariant(),
                user.State
            };

            await this.dbConnection.ExecuteAsync(UPDATE_USER_SQL, newUserAndPassword);
        }

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