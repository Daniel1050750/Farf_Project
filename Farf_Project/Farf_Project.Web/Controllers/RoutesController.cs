using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Farf_Project.Core;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Farf_Project.Web
{
    public class RoutesController : Controller
    {
        #region Private Readonly Variables

        private readonly IRoutesService routesService;

        #endregion Private Readonly Variables

        #region Constructor

        public RoutesController(IRoutesService routesService)
        {
            this.routesService = routesService;
        }

        #endregion Constructor

        #region Public Methods
        /// <summary>
        /// Get all routes
        /// </summary>
        /// <returns></returns>
        [Authorize("administrator")]
        [HttpGet("api/routes")]
        public async Task<IActionResult> GetRoutesAsync()
        {
            var routes = await this.routesService.GetRoutesListAsync();
            var result = routes.Select(u => RouteResource.Map(u));
            return this.Ok(result);
        }

        /// <summary>
        /// Get best delivery route
        /// </summary>
        /// <param name="startpoint"></param>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("/api/routes/create")]
        public IActionResult GetNewRoute([FromQuery]Guid startpoint, [FromQuery]Guid endpoint)
        {
            //Enum.TryParse(type, out SequenceType sequenceType);
            //Sequence seq = this.sequencesService.CreateSequence(name, description, sequenceType);
            //SequenceResource seqRes = SequenceResource.Map(seq);
            //return this.Ok(seqRes);
            return null;
        }

        /// <summary>
        /// Get route by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize("administrator")]
        [HttpGet("api/routes/{id}")]
        public async Task<IActionResult> GetRouteAsync(Guid id)
        {
            var route = await this.routesService.GetRouteAsync(id);
            var routeRes = RouteResource.Map(route);
            return this.Ok(routeRes);
        }

        /// <summary>
        /// Create new route
        /// </summary>
        /// <param name="routeResource"></param>
        [Authorize("administrator")]
        [HttpPost("api/routes")]
        public async Task<IActionResult> CreateRouteAsync([FromBody] RouteResource routeResource)
        {
            var route = RouteResource.Map(routeResource);
            await this.routesService.CreateRouteAsync(route);
            return this.Ok();
        }

        /// <summary>
        /// Delete route
        /// </summary>
        /// <param name="id"></param>
        [Authorize("administrator")]
        [HttpDelete("api/routes/{id}")]
        public async Task<IActionResult> DeleteRouteAsync(Guid id)
        {
            await this.routesService.DeleteRouteAsync(id);
            return this.Ok();
        }

        /// <summary>
        /// Update route data
        /// </summary>
        /// <param name="routeResource"></param>
        [Authorize("administrator")]
        [HttpPut("api/routes")]
        public async Task<IActionResult> UpdatetRouteAsync([FromBody] RouteResource routeResource)
        {
            var route = RouteResource.Map(routeResource);
            await this.routesService.UpdateRouteAsync(route);
            return this.Ok();
        }
        #endregion Public Methods
    }
}