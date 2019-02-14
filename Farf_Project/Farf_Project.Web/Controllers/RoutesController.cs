using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Farf_Project.Core;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Farf_Project.Web
{
    [Authorize]
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

        [Authorize("administrator")]
        [HttpGet("api/routes")]
        public async Task<IActionResult> GetRoutesAsync()
        {
            var routes = await this.routesService.GetRoutesListAsync();
            var result = routes.Select(u => RouteResource.Map(u));
            return this.Ok(result);
        }

        [Authorize]
        [HttpGet("api/routes/{id}")]
        public async Task<IActionResult> GetRouteAsync(Guid id)
        {
            var route = await this.routesService.GetRouteAsync(id);
            var routeRes = RouteResource.Map(route);
            return this.Ok(routeRes);
        }

        [Authorize("administrator")]
        [HttpPost("api/routes")]
        public async Task<IActionResult> CreateRouteAsync([FromBody] RouteResource routeResource)
        {
            var route = RouteResource.Map(routeResource);
            await this.routesService.CreateRouteAsync(route, routeResource.Password);
            return this.Ok();
        }

        [Authorize("administrator")]
        [HttpDelete("api/routes/{id}")]
        public async Task<IActionResult> DeleteRouteAsync(Guid id)
        {
            await this.routesService.DeleteRouteAsync(id);
            return this.Ok();
        }

        [Authorize("administrator")]
        [HttpPut("api/routes")]
        public async Task<IActionResult> UpdatetRouteAsync([FromBody] RouteResource routeResource)
        {
            var route = RouteResource.Map(routeResource);
            await this.routesService.UpdateRouteAsync(route, routeResource.Password);
            return this.Ok();
        }

        #endregion Public Methods
    }
}