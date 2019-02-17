using System;

namespace Farf_Project.Core
{
    /// <summary>
    /// The route
    /// </summary>
    public class Route
    {
        /// <summary>
        /// Gets or sets the route identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the route name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the route start point
        /// </summary>
        public Guid PointStart { get; set; }

        /// <summary>
        /// Gets or sets the route end point
        /// </summary>
        public Guid PointEnd { get; set; }

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
        public RouteState State { get; set; }

        /// <summary>
        /// Gets or sets the created on date
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the updated on date
        /// </summary>
        public DateTime UpdatedOn { get; set; }
    }
}