using System;

namespace Farf_Project.Core
{
    /// <summary>
    /// The throwed exeption when there are an irregular operations over the routes.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class RouteException: Exception
    {
        /// <summary>
        /// Gets the route exception type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public RouteExceptionType Type { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RouteException"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public RouteException(RouteExceptionType type) => this.Type = type;
    }

    /// <summary>
    /// The route exception types.
    /// </summary>
    public enum RouteExceptionType
    {
        RouteNotFound = 1,
        InvalidPassword,
        RouteNotActive,
        RoleNotAdmin
    }
}
