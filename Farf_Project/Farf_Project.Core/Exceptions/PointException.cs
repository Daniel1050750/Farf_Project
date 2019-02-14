using System;

namespace Farf_Project.Core
{
    /// <summary>
    /// The throwed exeption when there are an irregular operations over the points.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class PointException: Exception
    {
        /// <summary>
        /// Gets the point exception type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public PointExceptionType Type { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointException"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public PointException(PointExceptionType type) => this.Type = type;
    }

    /// <summary>
    /// The point exception types.
    /// </summary>
    public enum PointExceptionType
    {
        PointNotFound = 1,
        InvalidPassword,
        PointNotActive,
        RoleNotAdmin
    }
}
