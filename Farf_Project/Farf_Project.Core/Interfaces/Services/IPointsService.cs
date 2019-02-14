using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Farf_Project.Core
{
    public interface IPointsService
    {
        /// <summary>
        /// Get all points asynchronous.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        Task<Point> GetPointsAsync(Point point);

        /// <summary>
        /// Create a new point asynchronous.
        /// </summary>
        /// <param name="point"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task CreatePointAsync(Point point);

        /// <summary>
        /// Delete a point by ID asynchronous.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeletePointAsync(Guid id);

        /// <summary>
        ///  
        /// </summary>
        /// <returns></returns>
        Task<IList<Point>> GetPointsListAsync();

        /// <summary>
        /// Get point by pointname
        /// </summary>
        /// <param name="pointname"></param>
        /// <returns></returns>
        Task GetPointAsync(string pointname);

        /// <summary>
        /// Gets the point by pointname asynchronous.
        /// </summary>
        /// <param name="pointname">The pointname.</param>
        /// <returns>The point.</returns>
        Task<Point> GetPointByPointnameAsync(string pointname);

        /// <summary>
        /// Gets the point asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The point.</returns>
        Task<Point> GetPointAsync(Guid id);

        /// <summary>
        /// Update point data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="point"></param>
        /// <param name="password"></param>
        /// <param name="repassword"></param>
        /// <returns></returns>
        Task UpdatePointAsync(Point point);
    }
}
