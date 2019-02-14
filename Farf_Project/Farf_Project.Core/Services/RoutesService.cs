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
    public class RoutesService : IRoutesService
    {
        #region Private Readonly Variable

        private readonly IRoutesRepository routesRepository;

        #endregion Private Readonly Variable

        #region Private Constants

        private const int MAX_INPUT_LENGTH = 255;
        private const int MIN_INPUT_LENGTH = 4;
        private const string VALID_USERNAME_PATTERN = @"^[a-zA-Z0-9_@.-]*$";

        #endregion Private Constants

        #region Constructor

        public RoutesService(IRoutesRepository routesRepository)
        {
            this.routesRepository = routesRepository;
        }

        #endregion Constructor

        /// <summary>
        /// Get route
        /// Not implemented yet
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>
        public Task<Route> GetRoutes(Route route)
        {
            return null;
        }

        /// <summary>
        /// Gets the route.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The route.</returns>
        public async Task<Route> GetRouteAsync(Guid id)
        {
            if (Guid.Empty.Equals(id))
            {
                throw new ArgumentNullException("The id can not be empty.");
            }

            var route = await this.routesRepository.GetRouteAsync(id);

            if (route == null)
            {
                throw new InvalidArgumentException("This route don't existe anymore.");
            }
            else
            {
                return route;
            }

            
        }


        /// <summary>
        /// Gets the route by routename asynchronous.
        /// </summary>
        /// <param name="routename">The routename.</param>
        /// <returns>The route.</returns>
        /// <exception cref="ArgumentNullException">The routename parameter can not be null.</exception>
        public async Task<Route> GetRouteByRoutenameAsync(string routename)
        {
            if (string.IsNullOrEmpty(routename))
            {
                throw new ArgumentNullException("The routename parameter can not be null.");
            }

            var route = await this.routesRepository.GetRouteByRoutenameAsync(routename);

            return route;
        }

        /// <summary>
        /// Creates the route asynchronous.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public async Task CreateRouteAsync(Route route, string password)
        {
            await this.ValidateCreateRouteAsync(route, password);

            // creates a new Id to the route
            route.Id = Guid.NewGuid();

            // get random salt and build a secure password hash with the random salt.
            var salt = BuildRandomSalt();
            var securePassword = BuildSecurePassword(password, salt);

            // stores the new route
            await this.routesRepository.CreateRouteAsync(route, securePassword, salt);
        }

        /// <summary>
        /// Deletes the route by ID asynchronously.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteRouteAsync(Guid id)
        {
            await this.routesRepository.DeleteRouteAsync(id);
        }

        /// <summary>
        /// Get route by routename asynchronous.
        /// </summary>
        /// <param name="routename"></param>
        /// <returns></returns>
        public async Task GetRouteAsync(string routename)
        {
            await this.routesRepository.GetRouteByRoutenameAsync(routename);
        }

        /// <summary>
        /// Get list of routes asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<Route>> GetRoutesListAsync()
        {
            var routes = await this.routesRepository.GetRoutesAsync();
            return routes.ToList();
        }

        public async Task UpdateRouteAsync(Route route, string password)
        {
            await this.ValidateUpdateRouteAsync(route);

            if (!string.IsNullOrEmpty(password))
            {
                // get random salt and build a secure password hash with the random salt.
                string salt = BuildRandomSalt();
                string securePassword = BuildSecurePassword(password, salt);
                await this.routesRepository.UpdateRoutePasswordAsync(route.Id, securePassword, salt);
            }
            await this.routesRepository.UpdateRouteAsync(route);
        }

        /// <summary>
        /// Validates the credentials asynchronous.
        /// </summary>
        /// <param name="routename">The routename.</param>
        /// <param name="password">The password.</param>
        /// <returns>
        /// </returns>
        /// <exception cref="ArgumentNullException">The routename parameter can not be null.
        /// or
        /// The password parameter can not be null.</exception>
        /// <exception cref="Neadvance.Core.RouteException">
        /// </exception>
        public async Task ValidateCredentialsAsync(string routename, string password)
        {
            if (string.IsNullOrEmpty(routename))
            {
                throw new ArgumentNullException("The routename parameter can not be null.");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("The password parameter can not be null.");
            }

            var route = await this.GetRouteByRoutenameAsync(routename);

            // the route does not exists for the provided routename
            if (route == null)
            {
                throw new RouteException(RouteExceptionType.RouteNotFound);
            }
            
            // verify if the route is active
            if (route.State != RouteState.Active)
            {
                throw new RouteException(RouteExceptionType.RouteNotActive);
            }

            // get salt from the route
            var passwordSalt = await this.routesRepository.GetPasswordSaltAsync(route.Id);

            // generate the secure version of password
            var securePassword = BuildSecurePassword(password, passwordSalt);

            // verify if the password matchs
            var validCredentials = await this.routesRepository.VerifyPasswordAsync(route.Id, securePassword);

            if (!validCredentials)
            {
                throw new RouteException(RouteExceptionType.InvalidPassword);
            }
        }

        #region Private Methods

        private void ValidateRoute(Route route)
        {
            var name = route.Name.Trim();

            if (route == null)
            {
                throw new MissingArgumentException("The route can't be null.");
            }

            if (name.Replace("\n", string.Empty).Length > MAX_INPUT_LENGTH && name.Replace("\n", string.Empty).Length < MIN_INPUT_LENGTH)
            {
                throw new InvalidArgumentException(string.Format("The routename length must be between {0} and {1} characters", MIN_INPUT_LENGTH, MAX_INPUT_LENGTH));
            }

            if (!Regex.IsMatch(name, VALID_USERNAME_PATTERN))
            {
                throw new InvalidArgumentException("Allowed characters: a-z A-Z 0-9");
            }

            if (!Enum.IsDefined(typeof(RouteState), route.State))
            {
                throw new InvalidArgumentException("State not allowed.");
            }
        }

        private async Task ValidateUpdateRouteAsync(Route route)
        {
            this.ValidateRoute(route);
           
            var resID = await this.routesRepository.GetRouteAsync(route.Id);

            if (resID == null)
            {
                throw new InvalidArgumentException("Route doesn't exist.");
            }

            var res = await this.routesRepository.GetRouteByRoutenameAsync(route.Name);

            if (res != null && res.Id != route.Id)
            {
                throw new InvalidArgumentException("Routename already in use.");
            }
        }

        /// <summary>
        /// Validate password on route creation
        /// </summary>
        /// <param name="password"></param>
        private async Task ValidateCreateRouteAsync(Route route, string password)
        {
            this.ValidateRoute(route);
            if (password.Length < MIN_INPUT_LENGTH || password.Length > MAX_INPUT_LENGTH)
            {
                throw new InvalidArgumentException(string.Format("The password must have at least {0} characters and {1} maximum", MIN_INPUT_LENGTH, MAX_INPUT_LENGTH));
            }

            var res = await this.routesRepository.GetRouteByRoutenameAsync(route.Name);

            if (res != null)
            {
                throw new InvalidArgumentException("Routename already in use.");
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
