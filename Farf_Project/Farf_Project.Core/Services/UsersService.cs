using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Farf_Project.Core
{
    public class UsersService : IUsersService
    {
        #region Private Readonly Variable

        private readonly IUsersRepository usersRepository;

        #endregion Private Readonly Variable

        #region Private Constants

        private const int MAX_INPUT_LENGTH = 255;
        private const int MIN_INPUT_LENGTH = 4;
        private const string VALID_USERNAME_PATTERN = @"^[a-zA-Z0-9_@.-]*$";

        #endregion Private Constants

        #region Constructor

        public UsersService(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        #endregion Constructor

        /// <summary>
        /// Get user
        /// Not implemented yet
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<User> GetUsers(User user)
        {
            return null;
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The user.</returns>
        public async Task<User> GetUserAsync(Guid id)
        {
            if (Guid.Empty.Equals(id))
            {
                throw new ArgumentNullException("The id can not be empty.");
            }

            var user = await this.usersRepository.GetUserAsync(id);

            if (user == null)
            {
                throw new InvalidArgumentException("This user don't existe anymore.");
            }
            else
            {
                return user;
            }


        }


        /// <summary>
        /// Gets the user by username asynchronous.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>The user.</returns>
        /// <exception cref="ArgumentNullException">The username parameter can not be null.</exception>
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException("The username parameter can not be null.");
            }

            var user = await this.usersRepository.GetUserByUsernameAsync(username);

            return user;
        }

        /// <summary>
        /// Creates the user asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public async Task CreateUserAsync(User user, string password)
        {
            await this.ValidateCreateUserAsync(user, password);

            // creates a new Id to the user
            user.Id = Guid.NewGuid();

            // get random salt and build a secure password hash with the random salt.
            var salt = BuildRandomSalt();
            var securePassword = BuildSecurePassword(password, salt);

            // stores the new user
            await this.usersRepository.CreateUserAsync(user, securePassword, salt);
        }

        /// <summary>
        /// Deletes the user by ID asynchronously.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteUserAsync(Guid id)
        {
            await this.usersRepository.DeleteUserAsync(id);
        }

        /// <summary>
        /// Get user by username asynchronous.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task GetUserAsync(string username)
        {
            await this.usersRepository.GetUserByUsernameAsync(username);
        }

        /// <summary>
        /// Get list of users asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<User>> GetUsersListAsync()
        {
            var users = await this.usersRepository.GetUsersAsync();
            return users.ToList();
        }

        public async Task UpdateUserAsync(User user, string password)
        {
            await this.ValidateUpdateUserAsync(user, password);

            if (!string.IsNullOrEmpty(password))
            {
                // get random salt and build a secure password hash with the random salt.
                string salt = BuildRandomSalt();
                string securePassword = BuildSecurePassword(password, salt);
                await this.usersRepository.UpdateUserPasswordAsync(user.Id, securePassword, salt);
            }
            await this.usersRepository.UpdateUserAsync(user);
        }

        /// <summary>
        /// Validates the credentials asynchronous.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>
        /// </returns>
        /// <exception cref="ArgumentNullException">The username parameter can not be null.
        /// or
        /// The password parameter can not be null.</exception>
        /// <exception cref="Farf_Project.Core.UserException">
        /// </exception>
        public async Task ValidateCredentialsAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException("The username parameter can not be null.");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("The password parameter can not be null.");
            }

            var user = await this.GetUserByUsernameAsync(username);

            // the user does not exists for the provided username
            if (user == null)
            {
                throw new UserException(UserExceptionType.UserNotFound);
            }

            // verify if the user is active
            if (user.State != UserState.Active)
            {
                throw new UserException(UserExceptionType.UserNotActive);
            }

            // get salt from the user
            var passwordSalt = await this.usersRepository.GetPasswordSaltAsync(user.Id);

            // generate the secure version of password
            var securePassword = BuildSecurePassword(password, passwordSalt);

            // verify if the password matchs
            var validCredentials = await this.usersRepository.VerifyPasswordAsync(user.Id, securePassword);

            if (!validCredentials)
            {
                throw new UserException(UserExceptionType.InvalidPassword);
            }
        }

        #region Private Methods
        /// <summary>
        /// Generic validate user data
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        private void ValidateUser(User user)
        {
            var username = user.Username.Trim();

            if (user == null)
            {
                throw new MissingArgumentException("The user can't be null.");
            }

            if (username.Replace("\n", string.Empty).Length > MAX_INPUT_LENGTH && username.Replace("\n", string.Empty).Length < MIN_INPUT_LENGTH)
            {
                throw new InvalidArgumentException(string.Format("The username length must be between {0} and {1} characters", MIN_INPUT_LENGTH, MAX_INPUT_LENGTH));
            }

            if (!Regex.IsMatch(username, VALID_USERNAME_PATTERN))
            {
                throw new InvalidArgumentException("Allowed characters: a-z A-Z 0-9");
            }

            // the user role does not set
            if (!Enum.IsDefined(typeof(UserState), user.State))
            {
                throw new InvalidArgumentException("Role not allowed.");
            }

            if (!Enum.IsDefined(typeof(UserState), user.State))
            {
                throw new InvalidArgumentException("State not allowed.");
            }
        }

        /// <summary>
        /// Validate password on user update
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private async Task ValidateUpdateUserAsync(User user, string password)
        {
            this.ValidateUser(user);
            if (password.Length != 0 && (password.Length < MIN_INPUT_LENGTH || password.Length > MAX_INPUT_LENGTH))
            {
                throw new InvalidArgumentException(string.Format("The password length must be between {0} and {1} characters", MIN_INPUT_LENGTH, MAX_INPUT_LENGTH));
            }

            var resID = await this.usersRepository.GetUserAsync(user.Id);

            if (resID == null)
            {
                throw new InvalidArgumentException("User doesn't exist.");
            }

            var res = await this.usersRepository.GetUserByUsernameAsync(user.Username);

            if (res != null && res.Id != user.Id)
            {
                throw new InvalidArgumentException("Username already in use.");
            }
        }

        /// <summary>
        /// Validate password on user creation
        /// </summary>
        /// <param name="password"></param>
        private async Task ValidateCreateUserAsync(User user, string password)
        {
            this.ValidateUser(user);
            if (password.Length < MIN_INPUT_LENGTH || password.Length > MAX_INPUT_LENGTH)
            {
                throw new InvalidArgumentException(string.Format("The password must have at least {0} characters and {1} maximum", MIN_INPUT_LENGTH, MAX_INPUT_LENGTH));
            }

            var res = await this.usersRepository.GetUserByUsernameAsync(user.Username);

            if (res != null)
            {
                throw new InvalidArgumentException("Username already in use.");
            }
        }

        /// <summary>
        /// Build secure password
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        private static string BuildSecurePassword(string password, string salt)
        {
            // transform the password to a byte array
            var passwordBytes = Encoding.UTF8.GetBytes(password);

            // convert salt to bytes
            byte[] saltBytes = new byte[salt.Length / 2];
            for (int index = 0; index < saltBytes.Length; index++)
            {
                string byteValue = salt.Substring(index * 2, 2);
                saltBytes[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }

            // join the password and salt bytes
            var saltedPassword = passwordBytes.Concat(saltBytes).ToArray();

            // apply hash to the salted password
            byte[] hashedPassword;
            using (var sha512 = SHA512.Create())
            {
                hashedPassword = sha512.ComputeHash(saltedPassword);
            }

            // convert salt and password byte array to string
            var securePassword = BitConverter.ToString(hashedPassword).Replace("-", string.Empty);

            return securePassword;
        }

        /// <summary>
        /// Build random salt password
        /// </summary>
        /// <returns></returns>
        private static string BuildRandomSalt()
        {
            var saltBytes = new Byte[64];
            using (var rp = new RNGCryptoServiceProvider())
            {
                rp.GetBytes(saltBytes);
            }

            return BitConverter.ToString(saltBytes).Replace("-", string.Empty);
        }

        #endregion Private Methods
    }
}