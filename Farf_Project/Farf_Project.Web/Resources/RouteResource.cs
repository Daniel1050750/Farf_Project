using Farf_Project.Core;
using System;

namespace Farf_Project.Web
{
    public class RouteResource
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Routename { get; set; }
        public string Password { get; set; }
        public string State { get; set; }
        public string LastAuthentication { get; set; }

        #region Mappers

        /// <summary>
        /// Route to RouteResource
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static RouteResource Map(Route source)
        {
            if (source == null)
            {
                return null;
            }

            var target = new RouteResource
            {
                Id = GuidHelper.GuidToString(source.Id),
                Name = source.Name,
                LastAuthentication = source.LastAuthentication.HasValue ? DateTimeHelper.ConvertDateTimeToString(source.LastAuthentication.Value) : "",
                State = source.State.ToString()
            };

            return target;
        }

        /// <summary>
        /// RouteResource to route
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Route Map(RouteResource source)
        {
            if (source == null)
            {
                return null;
            }

            var target = new Route
            {
                Id = GuidHelper.StringToGuid(source.Id),
                Name = source.Name
            };

            Enum.TryParse(source.State, out RouteState routeState);
            target.State = routeState;

            return target;
        }

        #endregion Mappers
    }
}