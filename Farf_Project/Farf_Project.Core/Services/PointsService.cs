using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Farf_Project.Core
{
    public class PointsService : IPointsService
    {
        #region Private Readonly Variable

        private readonly IPointsRepository pointsRepository;
        private readonly IRoutesRepository routesRepository;

        #endregion Private Readonly Variable

        #region Private Constants

        private const int MAX_INPUT_LENGTH = 255;
        private const int MIN_INPUT_LENGTH = 4;

        #endregion Private Constants

        #region Constructor

        public PointsService(IPointsRepository pointsRepository, IRoutesRepository routesRepository)
        {
            this.pointsRepository = pointsRepository;
            this.routesRepository = routesRepository;
        }

        #endregion Constructor
        /// <summary>
        /// Get point by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Point> GetPointAsync(Guid id)
        {
            if (Guid.Empty.Equals(id))
            {
                throw new ArgumentNullException("The id can not be empty.");
            }

            var point = await this.pointsRepository.GetPointAsync(id);

            if (point == null)
            {
                throw new InvalidArgumentException("This point don't existe anymore.");
            }
            else
            {
                return point;
            }
        }

        /// <summary>
        /// Get point by name
        /// </summary>
        /// <param name="pointname"></param>
        /// <returns></returns>
        public async Task<Point> GetPointByPointnameAsync(string pointname)
        {
            if (string.IsNullOrEmpty(pointname))
            {
                throw new ArgumentNullException("The pointname parameter can not be null.");
            }
            var point = await this.pointsRepository.GetPointByPointnameAsync(pointname);
            return point;
        }

        /// <summary>
        /// Create new point
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public async Task CreatePointAsync(Point point)
        {
            await this.ValidateCreatePointAsync(point);

            // creates a new Id to the point
            point.Id = Guid.NewGuid();

            // stores the new point
            await this.pointsRepository.CreatePointAsync(point);
        }

        /// <summary>
        /// Delete point
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeletePointAsync(Guid id)
        {
            await this.ValidateDeletePoint(id);
            await this.pointsRepository.DeletePointAsync(id);
        }

        /// <summary>
        /// Get all active points
        /// </summary>
        /// <returns>Point list</returns>
        public async Task<IList<Point>> GetPointsListAsync()
        {
            var points = await this.pointsRepository.GetPointsAsync();
            return points.ToList();
        }

        /// <summary>
        /// Update route data
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public async Task UpdatePointAsync(Point point)
        {
            await this.ValidateUpdatePointAsync(point);
            await this.pointsRepository.UpdatePointAsync(point);
        }

        #region Private Methods
        /// <summary>
        /// Generic validate point data
        /// </summary>
        /// <param name="point"></param>
        /// <param name="password"></param>
        private void ValidatePoint(Point point)
        {
            var name = point.Name.Trim();
            var addr = point.Address.Trim();

            if (point == null)
            {
                throw new MissingArgumentException("The point can't be null.");
            }

            if (name.Replace("\n", string.Empty).Length > MAX_INPUT_LENGTH || name.Replace("\n", string.Empty).Length < MIN_INPUT_LENGTH)
            {
                throw new InvalidArgumentException(string.Format("The point name length must be between {0} and {1} characters", MIN_INPUT_LENGTH, MAX_INPUT_LENGTH));
            }

            if (addr.Replace("\n", string.Empty).Length > MAX_INPUT_LENGTH || addr.Replace("\n", string.Empty).Length < MIN_INPUT_LENGTH)
            {
                throw new InvalidArgumentException(string.Format("The point address length must be between {0} and {1} characters", MIN_INPUT_LENGTH, MAX_INPUT_LENGTH));
            }

            if (!Enum.IsDefined(typeof(PointState), point.State))
            {
                throw new InvalidArgumentException("State not allowed.");
            }
        }

        /// <summary>
        /// Validate point on create
        /// </summary>
        /// <param name="point"></param>
        private async Task ValidateUpdatePointAsync(Point point)
        {
            this.ValidatePoint(point);

            var resID = await this.pointsRepository.GetPointAsync(point.Id);

            if (resID == null)
            {
                throw new InvalidArgumentException("Point doesn't exist.");
            }

            var res = await this.pointsRepository.GetPointByPointnameAsync(point.Name);

            if (res != null && res.Id != point.Id)
            {
                throw new InvalidArgumentException("Point already in use.");
            }
        }

        /// <summary>
        /// Validate point on update
        /// </summary>
        /// <param name="point"></param>
        private async Task ValidateCreatePointAsync(Point point)
        {
            this.ValidatePoint(point);

            var res = await this.pointsRepository.GetPointByPointnameAsync(point.Name);

            if (res != null)
            {
                throw new InvalidArgumentException("Name already in use.");
            }
        }

        private async Task ValidateDeletePoint(Guid id)
        {
            var res = await this.routesRepository.GetPointOnRoute(id);

            if (res != null)
            {
                throw new InvalidArgumentException("This point is in used.");
            }
        }
        #endregion Private Methods
    }
}
