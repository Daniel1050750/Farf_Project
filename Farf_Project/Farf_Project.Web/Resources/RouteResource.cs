using Farf_Project.Core;
using System;

namespace Farf_Project.Web
{
    public class RouteResource
    {
        /// <summary>
        /// Gets or sets the route identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the route name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the route start point
        /// </summary>
        public string PointStart { get; set; }

        /// <summary>
        /// Gets or sets the route end point
        /// </summary>
        public string PointEnd { get; set; }

        /// <summary>
        /// Gets or sets the route price
        /// </summary>
        public int RoutePrice { get; set; }

        /// <summary>
        /// Gets or sets the route time
        /// </summary>
        public int RouteTime { get; set; }

        /// <summary>
        /// Gets or sets the route state
        /// </summary>
        public string State { get; set; }

        #region Mappers

        /// <summary>
        /// Route to RouteResource
        /// </summary>
        /// <param name="source"></param>
        /// <returns>RouteResource</returns>
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
                PointStart = GuidHelper.GuidToString(source.PointStart),
                PointEnd = GuidHelper.GuidToString(source.PointEnd),
                RoutePrice = source.RoutePrice,
                RouteTime = source.RouteTime,
                State = source.State.ToString()
            };

            return target;
        }

        /// <summary>
        /// RouteResource to Route
        /// </summary>
        /// <param name="source"></param>
        /// <returns>Route</returns>
        public static Route Map(RouteResource source)
        {
            if (source == null)
            {
                return null;
            }

            var target = new Route
            {
                Id = GuidHelper.StringToGuid(source.Id),
                Name = source.Name,
                PointStart = GuidHelper.StringToGuid(source.PointStart),
                PointEnd = GuidHelper.StringToGuid(source.PointEnd),
                RoutePrice = source.RoutePrice,
                RouteTime = source.RouteTime,
            };

            Enum.TryParse(source.State, out RouteState routeState);
            target.State = routeState;

            return target;
        }
        #endregion Mappers
    }
}