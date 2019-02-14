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
        /// <returns></returns>
        Task<User> GetUsers(User user);

        /// <summary>
        /// Create a new user asynchronous.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task CreateUserAsync(User user, string password);

        /// <summary>
        /// Delete a user by ID asynchronous.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteUserAsync(Guid id);

        /// <summary>
        ///  
        /// </summary>
        /// <returns></returns>
        Task<IList<User>> GetUsersListAsync();

        /// <summary>
        /// Get user by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task GetUserAsync(string username);
        /// <summary>
        /// Gets the user by username asynchronous.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>The user.</returns>
        Task<User> GetUserByUsernameAsync(string username);

        /// <summary>
        /// Gets the user asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The user.</returns>
        Task<User> GetUserAsync(Guid id);

        /// <summary>
        /// Validates the credentials.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        Task ValidateCredentialsAsync(string username, string password);

        /// <summary>
        /// Update user data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="repassword"></param>
        /// <returns></returns>
        Task UpdateUserAsync(User user, string password);
    }
}
