using Farf_Project.Core;
using System;

namespace Farf_Project.Web
{
    public class PointResource
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Pointname { get; set; }
        public string Password { get; set; }
        public string State { get; set; }
        public string LastAuthentication { get; set; }

        #region Mappers

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
                LastAuthentication = source.LastAuthentication.HasValue ? DateTimeHelper.ConvertDateTimeToString(source.LastAuthentication.Value) : "",
                State = source.State.ToString()
            };

            return target;
        }

        public static Point Map(PointResource source)
        {
            if (source == null)
            {
                return null;
            }

            var target = new Point
            {
                Id = GuidHelper.StringToGuid(source.Id),
                Name = source.Name
            };

            Enum.TryParse(source.State, out PointState pointState);
            target.State = pointState;

            return target;
        }

        #endregion Mappers
    }
}