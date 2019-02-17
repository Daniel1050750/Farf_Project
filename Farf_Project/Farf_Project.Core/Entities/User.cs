using System;

namespace Farf_Project.Core
{
    /// <summary>
    /// The user
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the user identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the role
        /// </summary>
        public UserRole Role { get; set; }

        /// <summary>
        /// Gets or sets the state
        /// </summary>
        public UserState State { get; set; }

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