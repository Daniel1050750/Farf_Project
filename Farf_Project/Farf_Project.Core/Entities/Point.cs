using System;

namespace Farf_Project.Core
{
    /// <summary>
    /// The point
    /// </summary>
    public class Point
    {
        /// <summary>
        /// Gets or sets the point identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the point name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the pointname
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the state
        /// </summary>
        public PointState State { get; set; }

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