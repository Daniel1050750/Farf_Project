using System;

namespace Farf_Project.Core
{
    /// <summary>
    /// The point.
    /// </summary>
    public class Point
    {
        /// <summary>
        /// Gets or sets the point identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the point name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the pointname.
        /// </summary>
        /// <value>
        /// The pointname.
        /// </value>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public PointState State { get; set; }

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