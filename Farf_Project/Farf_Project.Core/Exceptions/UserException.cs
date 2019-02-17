using System;

namespace Farf_Project.Core
{
    /// <summary>
    /// The throwed exeption when there are an irregular operations over the users.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class UserException: Exception
    {
        /// <summary>
        /// Gets the user exception type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public UserExceptionType Type { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserException"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public UserException(UserExceptionType type) => this.Type = type;
    }

    /// <summary>
    /// The user exception types.
    /// </summary>
    public enum UserExceptionType
    {
        UserNotFound = 1,
        InvalidPassword,
        UserNotActive
    }
}
