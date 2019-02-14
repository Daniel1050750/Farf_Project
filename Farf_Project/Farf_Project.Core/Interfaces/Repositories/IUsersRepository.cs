using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Farf_Project.Core
{
    /// <summary>
    /// Repository to access, create and update Users on database.
    /// </summary>
    public interface IUsersRepository
    {
        /// <summary>
        /// Creates the user asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="password">The password.</param>
        /// <param name="passwordSalt">The password salt.</param>
        /// <returns></returns>
        Task CreateUserAsync(User user, string password, string passwordSalt);

        /// <summary>
        /// Gets the users asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<User>> GetUsersAsync();

        /// <summary>
        /// Gets the user asynchronous.
        /// </summary>
        /// <param name="id">The user id.</param>
        /// <returns>
        /// The user that matchs the id.
        /// </returns>
        Task<User> GetUserAsync(Guid id);

        /// <summary>
        /// Gets the user by username asynchronous.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>
        /// The user that matchs the username.
        /// </returns>
        Task<User> GetUserByUsernameAsync(string username);

        /// <summary>
        /// Gets the password salt asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The password salt.</returns>
        Task<string> GetPasswordSaltAsync(Guid userId);

        /// <summary>
        /// Verifies the password asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="password">The password.</param>
        /// <returns>True if the password is valid for the provided user, false otherwise.</returns>
        Task<bool> VerifyPasswordAsync(Guid userId, string password);

        /// <summary>
        /// Deletes the user asynchronously.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteUserAsync(Guid id);

        /// <summary>
        /// Update user data
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
        Task UpdateUserAsync(User user);

        /// <summary>
        /// Update user password
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
        Task UpdateUserPasswordAsync(Guid id, string securePassword, string salt);
    }
}