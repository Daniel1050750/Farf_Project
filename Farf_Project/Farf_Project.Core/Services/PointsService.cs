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

        #endregion Private Readonly Variable

        #region Private Constants

        private const int MAX_INPUT_LENGTH = 255;
        private const int MIN_INPUT_LENGTH = 4;
        private const string VALID_USERNAME_PATTERN = @"^[a-zA-Z0-9_@.-]*$";

        #endregion Private Constants

        #region Constructor

        public PointsService(IPointsRepository pointsRepository)
        {
            this.pointsRepository = pointsRepository;
        }

        #endregion Constructor

        public Task<Point> GetPointsAsync(Point point)
        {
            return null;
        }

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

        public async Task<Point> GetPointByPointnameAsync(string pointname)
        {
            if (string.IsNullOrEmpty(pointname))
            {
                throw new ArgumentNullException("The pointname parameter can not be null.");
            }

            var point = await this.pointsRepository.GetPointByPointnameAsync(pointname);

            return point;
        }

        public async Task CreatePointAsync(Point point)
        {
            await this.ValidateCreatePointAsync(point);

            // creates a new Id to the point
            point.Id = Guid.NewGuid();

            // stores the new point
            await this.pointsRepository.CreatePointAsync(point);
        }

        public async Task DeletePointAsync(Guid id)
        {
            await this.pointsRepository.DeletePointAsync(id);
        }

        public async Task GetPointAsync(string pointname)
        {
            await this.pointsRepository.GetPointByPointnameAsync(pointname);
        }

        public async Task<IList<Point>> GetPointsListAsync()
        {
            var points = await this.pointsRepository.GetPointsAsync();
            return points.ToList();
        }

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

            if (point == null)
            {
                throw new MissingArgumentException("The point can't be null.");
            }

            if (name.Replace("\n", string.Empty).Length > MAX_INPUT_LENGTH && name.Replace("\n", string.Empty).Length < MIN_INPUT_LENGTH)
            {
                throw new InvalidArgumentException(string.Format("The routename length must be between {0} and {1} characters", MIN_INPUT_LENGTH, MAX_INPUT_LENGTH));
            }

            if (!Regex.IsMatch(name, VALID_USERNAME_PATTERN))
            {
                throw new InvalidArgumentException("Allowed characters: a-z A-Z 0-9");
            }

            if (!Enum.IsDefined(typeof(PointState), point.State))
            {
                throw new InvalidArgumentException("State not allowed.");
            }
        }

        /// <summary>
        /// Validate password on point update
        /// </summary>
        /// <param name="point"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private async Task ValidateUpdatePointAsync(Point point)
        {
            this.ValidatePoint(point);

            var resID = await this.pointsRepository.GetPointAsync(point.Id);

            if (resID == null)
            {
                throw new InvalidArgumentException("Point doesn't exist.");
            }
        }

        private async Task ValidateCreatePointAsync(Point point)
        {
            this.ValidatePoint(point);

            var res = await this.pointsRepository.GetPointByPointnameAsync(point.Name);

            if (res != null)
            {
                throw new InvalidArgumentException("Name already in use.");
            }
        }

        #endregion Private Methods
    }
}
