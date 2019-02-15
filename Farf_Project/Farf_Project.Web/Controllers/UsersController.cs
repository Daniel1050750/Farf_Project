using System;
using System.Linq;
using Farf_Project.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Farf_Project.Web
{
    [Authorize]
    public class UsersController : Controller
    {
        #region Private Readonly Variables

        private readonly IUsersService usersService;

        #endregion Private Readonly Variables

        #region Constructor

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        #endregion Constructor

        #region Public Methods

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [Authorize("administrator")]
        [HttpGet("api/users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await this.usersService.GetUsersListAsync();
            var result = users.Select(u => UserResource.Map(u));
            return this.Ok(result);
        }

        /// <summary>
        /// Get user by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("api/users/{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await this.usersService.GetUserAsync(id);
            var userRes = UserResource.Map(user);
            return this.Ok(userRes);
        }

        /// <summary>
        /// Get user states
        /// </summary>
        /// <returns></returns>
        [HttpGet("api/users/states")]
        public IActionResult GetUserStates()
        {
            var types = Enum.GetNames(typeof(UserState));
            return this.Ok(types);
        }

        /// <summary>
        /// Get user roles
        /// </summary>
        /// <returns></returns>
        [HttpGet("api/users/roles")]
        public IActionResult GetUserRoles()
        {
            var types = Enum.GetNames(typeof(UserRole));
            return this.Ok(types);
        }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="userResource"></param>
        /// <returns></returns>
        [HttpPost("api/users")]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserResource userResource)
        {
            var user = UserResource.Map(userResource);
            await this.usersService.CreateUserAsync(user, userResource.Password);
            return this.Ok();
        }

        /// <summary>
        /// Delete user by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("api/users/{id}")]
        public async Task<IActionResult> DeleteUserAsync(Guid id)
        {
            await this.usersService.DeleteUserAsync(id);
            return this.Ok();
        }

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="userResource"></param>
        /// <returns></returns>
        [HttpPut("api/users")]
        public async Task<IActionResult> UpdatetUser([FromBody] UserResource userResource)
        {
            var user = UserResource.Map(userResource);
            await this.usersService.UpdateUserAsync(user, userResource.Password);
            return this.Ok();
        }

        #endregion Public Methods
    }
}