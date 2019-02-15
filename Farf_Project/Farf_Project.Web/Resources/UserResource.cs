using Farf_Project.Core;
using System;

namespace Farf_Project.Web
{
    public class UserResource
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string State { get; set; }
        public string Role { get; set; }
        public string LastAuthentication { get; set; }

        #region Mappers

        /// <summary>
        /// User to UserResource
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
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
                LastAuthentication = source.LastAuthentication.HasValue ? DateTimeHelper.ConvertDateTimeToString(source.LastAuthentication.Value) : "",
                State = source.State.ToString(),
                Role = source.Role.ToString()
            };

            return target;
        }

        /// <summary>
        /// UserResource to user
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
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