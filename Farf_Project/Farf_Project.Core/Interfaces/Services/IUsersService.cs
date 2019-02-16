using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Farf_Project.Core
{
    public interface IUsersService
    {
        /// <summary>
        /// Get all users asynchronous.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>User</returns>
        Task<User> GetUsers(User user);

        /// <summary>
        /// Create a new user asynchronous.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        Task CreateUserAsync(User user, string password);

        /// <summary>
        /// Delete a user by ID asynchronous.
        /// </summary>
        /// <param name="id"></param>
        Task DeleteUserAsync(Guid id);

        /// <summary>
        /// Get all active users
        /// </summary>
        Task<IList<User>> GetUsersListAsync();

        /// <summary>
        /// Get user by username
        /// </summary>
        /// <param name="username"></param>
        Task GetUserAsync(string username);
        
        /// <summary>
        /// Gets the user by username asynchronous.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>User</returns>
        Task<User> GetUserByUsernameAsync(string username);

        /// <summary>
        /// Gets the user asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>User</returns>
        Task<User> GetUserAsync(Guid id);

        /// <summary>
        /// Validates the credentials.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        Task ValidateCredentialsAsync(string username, string password);

        /// <summary>
        /// Update user data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="repassword"></param>
        Task UpdateUserAsync(User user, string password);
    }
}
