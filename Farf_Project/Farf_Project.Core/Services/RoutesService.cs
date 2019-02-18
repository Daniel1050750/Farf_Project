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
    public class RoutesService : IRoutesService
    {
        #region Private Readonly Variable

        private readonly IRoutesRepository routesRepository;
        private readonly IPointsRepository pointsRepository;

        #endregion Private Readonly Variable

        #region Private Constants
        List<List<Route>> routesList;
        List<Route> tempRouteList;
        private const int MAX_INPUT_LENGTH = 255;
        private const int MIN_INPUT_LENGTH = 4;

        #endregion Private Constants

        #region Constructor

        public RoutesService(IRoutesRepository routesRepository, IPointsRepository pointsRepository)
        {
            this.routesRepository = routesRepository;
            this.pointsRepository = pointsRepository;
        }

        #endregion Constructor

        /// <summary>
        /// Get route
        /// Not implemented yet
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>
        public Task<Route> GetRoutes(Route route)
        {
            return null;
        }

        /// <summary>
        /// Gets the route.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The route.</returns>
        public async Task<Route> GetRouteAsync(Guid id)
        {
            if (Guid.Empty.Equals(id))
            {
                throw new ArgumentNullException("The id can not be empty.");
            }

            var route = await this.routesRepository.GetRouteAsync(id);

            if (route == null)
            {
                throw new InvalidArgumentException("This route don't existe anymore.");
            }
            else
            {
                return route;
            }
        }


        /// <summary>
        /// Gets the route by routename asynchronous.
        /// </summary>
        /// <param name="routename">The routename.</param>
        /// <returns>The route.</returns>
        /// <exception cref="ArgumentNullException">The routename parameter can not be null.</exception>
        public async Task<Route> GetRouteByRoutenameAsync(string routename)
        {
            if (string.IsNullOrEmpty(routename))
            {
                throw new ArgumentNullException("The routename parameter can not be null.");
            }

            var route = await this.routesRepository.GetRouteByRoutenameAsync(routename);

            return route;
        }

        /// <summary>
        /// Creates the route asynchronous.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public async Task CreateRouteAsync(Route route)
        {
            await this.ValidateCreateRouteAsync(route);
            route.Id = Guid.NewGuid();
            await this.routesRepository.CreateRouteAsync(route);
        }

        /// <summary>
        /// Deletes the route by ID asynchronously.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteRouteAsync(Guid id)
        {
            await this.routesRepository.DeleteRouteAsync(id);
        }

        /// <summary>
        /// Get route by routename asynchronous.
        /// </summary>
        /// <param name="routename"></param>
        /// <returns></returns>
        public async Task GetRouteAsync(string routename)
        {
            await this.routesRepository.GetRouteByRoutenameAsync(routename);
        }

        /// <summary>
        /// Get list of routes asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<Route>> GetRoutesListAsync()
        {
            var routes = await this.routesRepository.GetRoutesAsync();
            return routes.ToList();
        }

        /// <summary>
        /// Update route
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>
        public async Task UpdateRouteAsync(Route route)
        {
            await this.ValidateUpdateRouteAsync(route);
            await this.routesRepository.UpdateRouteAsync(route);
        }

        /// <summary>
        /// Get delivery route
        /// </summary>
        /// <param name="sPoint"></param>
        /// <param name="ePoint"></param>
        /// <returns></returns>
        public async Task<IList<Route>> GetDeliveryRouteAsync(Guid sPoint, Guid ePoint)
        {
            if (Guid.Empty.Equals(sPoint))
            {
                throw new ArgumentNullException("The start point can not be empty.");
            }

            var pointStart = await this.pointsRepository.GetPointAsync(sPoint);

            if (pointStart == null)
            {
                throw new ArgumentNullException("The start point not exists anymore.");
            }

            if (Guid.Empty.Equals(ePoint))
            {
                throw new ArgumentNullException("The end point can not be empty.");
            }

            var endPoint = await this.pointsRepository.GetPointAsync(ePoint);

            if (endPoint == null)
            {
                throw new ArgumentNullException("The end point not exists anymore.");
            }

            if (ePoint == sPoint)
            {
                throw new ArgumentNullException("The start and end point can not be the same.");
            }

            var routes = await this.ValidateDeliveryRouteAsync(sPoint, ePoint);
            return routes.ToList();
        }

        #region Private Methods
        /// <summary>
        /// Generic route validations
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>
        private async Task ValidateRouteAsync(Route route)
        {
            var name = route.Name.Trim();

            if (route == null)
            {
                throw new MissingArgumentException("The route can't be null.");
            }

            if (name.Replace("\n", string.Empty).Length > MAX_INPUT_LENGTH || name.Replace("\n", string.Empty).Length < MIN_INPUT_LENGTH)
            {
                throw new InvalidArgumentException(string.Format("The routename length must be between {0} and {1} characters", MIN_INPUT_LENGTH, MAX_INPUT_LENGTH));
            }

            if (!Enum.IsDefined(typeof(RouteState), route.State))
            {
                throw new InvalidArgumentException("State not allowed.");
            }

            var startPoint = await this.pointsRepository.GetPointAsync(route.PointStart);

            if (startPoint == null)
            {
                throw new InvalidArgumentException("Start point not found.");
            }

            var endPoint = await this.pointsRepository.GetPointAsync(route.PointEnd);

            if (endPoint == null)
            {
                throw new InvalidArgumentException("End point not found.");
            }
        }

        /// <summary>
        /// Validate route data on update
        /// </summary>
        /// <param name="route"></param>
        private async Task ValidateUpdateRouteAsync(Route route)
        {
            await this.ValidateRouteAsync(route);
           
            var resID = await this.routesRepository.GetRouteAsync(route.Id);

            if (resID == null)
            {
                throw new InvalidArgumentException("Route doesn't exist.");
            }

            var res = await this.routesRepository.GetRouteByRoutenameAsync(route.Name);

            if (res != null && res.Id != route.Id)
            {
                throw new InvalidArgumentException("Route name already in use.");
            }
        }

        /// <summary>
        /// Validate route data on create
        /// </summary>
        /// <param name="password"></param>
        private async Task ValidateCreateRouteAsync(Route route)
        {
            await this.ValidateRouteAsync(route);

            var res = await this.routesRepository.GetRouteByRoutenameAsync(route.Name);

            if (res != null)
            {
                throw new InvalidArgumentException("Route name already in use.");
            }
        }

        /// <summary>
        /// Calculate all delivery route
        /// </summary>
        /// <param name="sPoint"></param>
        /// <param name="ePoint"></param>
        /// <returns></returns>
        private async Task<IList<Route>> ValidateDeliveryRouteAsync(Guid sPoint, Guid ePoint)
        {
            //var routesList = new List<Route>();

            var spRoutes = await this.routesRepository.GetRoutesWithStartPoint(sPoint);
            var epRoutes = await this.routesRepository.GetRoutesWithEndPoint(ePoint);

            if (spRoutes == null || epRoutes == null)
            {
                throw new InvalidArgumentException("Route can not be calculated.");
            }

            this.routesList = new List<List<Route>>();
            this.tempRouteList = new List<Route>();

            await this.RouteCalc(spRoutes, ePoint);

            var result = this.FilterBestRouteOption();
        
            return result;
        }

        /// <summary>
        /// Recursive method to validate routes 
        /// </summary>
        /// <param name="startRoute"></param>
        /// <param name="ePoint"></param>
        /// <returns></returns>
        private async Task RouteCalc(IEnumerable<Route> startRoute, Guid ePoint)
        {
            foreach (var item in startRoute)
            {
                this.tempRouteList.Add(item);
                if (item.PointEnd == ePoint)
                {
                    this.routesList.Add(this.tempRouteList);
                    this.tempRouteList = new List<Route>();
                }
                else
                {
                    var spRoutes = await this.routesRepository.GetRoutesWithStartPoint(item.PointEnd);
                    await this.RouteCalc(spRoutes, ePoint);
                }
            }
        }

        /// <summary>
        /// Filter data to retrive the best route
        /// </summary>
        /// <returns></returns>
        private List<Route> FilterBestRouteOption()
        {
            var price = 9999999;
            var time = 9999999;
            var result = new List<Route>();
            var filterFullList = this.routesList.Where(x => x.Count > 2);
            foreach (var item in filterFullList)
            {
                var newPrice = item.Sum(a => a.RoutePrice);
                var newTime = item.Sum(a => a.RouteTime);
                if (newPrice < price)
                {
                    result = item;
                }
                else if (newPrice == price)
                {
                    if(newTime < time)
                    {
                        result = item;
                    }
                }
            }
            return result;
        }
        #endregion Private Methods
    }
}
