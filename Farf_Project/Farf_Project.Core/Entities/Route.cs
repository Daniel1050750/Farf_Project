using System;

namespace Farf_Project.Core
{
    /// <summary>
    /// The route.
    /// </summary>
    public class Route
    {
        /// <summary>
        /// Gets or sets the route identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the route name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid RouteStart { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid RouteEnd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public RouteState State { get; set; }

        /// <summary>
        /// Gets or sets the last authentication date.
        /// </summary>
        /// <value>
        /// The last authentication.
        /// </value>
        public DateTime? LastAuthentication { get; set; }

        /// <summary>
        /// Gets or sets the created on date.
        /// </summary>
        /// <value>
        /// The created on.
        /// </value>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the updated on date.
        /// </summary>
        /// <value>
        /// The updated on.
        /// </value>
        public DateTime UpdatedOn { get; set; }
    }
}