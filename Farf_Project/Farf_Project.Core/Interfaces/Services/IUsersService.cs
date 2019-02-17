using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Farf_Project.Core
{
    public interface IUsersService
    {
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
        /// <returns>Users list</returns>
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
        /// Validates the credentials
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        Task ValidateCredentialsAsync(string username, string password);

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        Task UpdateUserAsync(User user, string password);
    }
}
