using Farf_Project.Core;
using System;

namespace Farf_Project.Web
{
    public class PointResource
    {
        /// <summary>
        /// Point identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Point name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Point address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Point state
        /// </summary>
        public string State { get; set; }

        #region Mappers
        /// <summary>
        /// Point to PointResource
        /// </summary>
        /// <param name="source"></param>
        /// <returns>PointResource</returns>
        public static PointResource Map(Point source)
        {
            if (source == null)
            {
                return null;
            }

            var target = new PointResource
            {
                Id = GuidHelper.GuidToString(source.Id),
                Name = source.Name,
                Address = source.Address,
                State = source.State.ToString()
            };

            return target;
        }

        /// <summary>
        /// PointResource to Point
        /// </summary>
        /// <param name="source"></param>
        /// <returns>Point</returns>
        public static Point Map(PointResource source)
        {
            if (source == null)
            {
                return null;
            }

            var target = new Point
            {
                Id = GuidHelper.StringToGuid(source.Id),
                Name = source.Name,
                Address = source.Address
            };

            Enum.TryParse(source.State, out PointState pointState);
            target.State = pointState;

            return target;
        }
        #endregion Mappers
    }
}