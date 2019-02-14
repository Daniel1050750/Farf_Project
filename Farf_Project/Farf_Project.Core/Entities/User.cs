using System;

namespace Farf_Project.Core
{
    /// <summary>
    /// The user.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the role identifier.
        /// </summary>
        /// <value>
        /// The role identifier.
        /// </value>
        public Guid RoleId { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public UserState State { get; set; }

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