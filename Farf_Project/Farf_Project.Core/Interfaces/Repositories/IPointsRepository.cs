using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Farf_Project.Core
{
    /// <summary>
    /// Repository to access, create and update Points on database.
    /// </summary>
    public interface IPointsRepository
    {
        /// <summary>
        /// Creates the point asynchronous
        /// </summary>
        /// <param name="point">The point</param>
        /// <param name="password">The password</param>
        /// <param name="passwordSalt">The password salt.</param>
        Task CreatePointAsync(Point point);

        /// <summary>
        /// Gets the points asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Point>> GetPointsAsync();

        /// <summary>
        /// Gets the point asynchronous.
        /// </summary>
        /// <param name="id">The point id.</param>
        /// <returns>
        /// The point that matchs the id.
        /// </returns>
        Task<Point> GetPointAsync(Guid id);

        /// <summary>
        /// Gets the point by pointname asynchronous.
        /// </summary>
        /// <param name="pointname">The pointname.</param>
        /// <returns>
        /// The point that matchs the pointname.
        /// </returns>
        Task<Point> GetPointByPointnameAsync(string pointname);

        /// <summary>
        /// Deletes the point asynchronously.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeletePointAsync(Guid id);

        /// <summary>
        /// Update point data
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
        Task UpdatePointAsync(Point point);
    }
}
