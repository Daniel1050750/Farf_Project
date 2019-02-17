using Farf_Project.Core;
using System;

namespace Farf_Project.Web
{
    public class UserResource
    {
        /// <summary>
        /// User identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// User state
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// USer role
        /// </summary>
        public string Role { get; set; }

        #region Mappers

        /// <summary>
        /// User to UserResource
        /// </summary>
        /// <param name="source"></param>
        /// <returns>UserResource</returns>
        public static UserResource Map(User source)
        {
            if (source == null)
            {
                return null;
            }

            var target = new UserResource
            {
                Id = GuidHelper.GuidToString(source.Id),
                Username = source.Username,
                State = source.State.ToString(),
                Role = source.Role.ToString()
            };

            return target;
        }

        /// <summary>
        /// UserResource to User
        /// </summary>
        /// <param name="source"></param>
        /// <returns>User</returns>
        public static User Map(UserResource source)
        {
            if (source == null)
            {
                return null;
            }

            var target = new User
            {
                Id = GuidHelper.StringToGuid(source.Id),
                Username = source.Username
            };

            Enum.TryParse(source.State, out UserState userState);
            target.State = userState;

            Enum.TryParse(source.Role, out UserRole userRole);
            target.Role = userRole;

            return target;
        }

        #endregion Mappers
    }
}