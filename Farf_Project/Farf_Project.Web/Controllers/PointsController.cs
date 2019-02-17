using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Farf_Project.Core;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Farf_Project.Web
{
    [Authorize("administrator")]
    public class PointsController : Controller
    {
        #region Private Readonly Variables

        private readonly IPointsService pointsService;

        #endregion Private Readonly Variables

        #region Constructor

        public PointsController(IPointsService pointsService)
        {
            this.pointsService = pointsService;
        }

        #endregion Constructor

        #region Public Methods       
        /// <summary>
        /// Get all points
        /// </summary>
        /// <returns>Points</returns>
        [HttpGet("api/points")]
        public async Task<IActionResult> GetPointsAsync()
        {
            var points = await this.pointsService.GetPointsListAsync();
            var result = points.Select(u => PointResource.Map(u));
            return this.Ok(result);
        }

        /// <summary>
        /// Get point by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Point</returns>
        [HttpGet("api/points/{id}")]
        public async Task<IActionResult> GetPointAsync(Guid id)
        {
            var point = await this.pointsService.GetPointAsync(id);
            var pointRes = PointResource.Map(point);
            return this.Ok(pointRes);
        }

        /// <summary>
        /// Create new point
        /// </summary>
        /// <param name="pointResource"></param>        
        [HttpPost("api/points")]
        public async Task<IActionResult> CreatePointAsync([FromBody] PointResource pointResource)
        {
            var point = PointResource.Map(pointResource);
            await this.pointsService.CreatePointAsync(point);
            return this.Ok();
        }

        /// <summary>
        /// Delete point
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("api/points/{id}")]
        public async Task<IActionResult> DeletePointAsync(Guid id)
        {
            await this.pointsService.DeletePointAsync(id);
            return this.Ok();
        }

        /// <summary>
        /// Update point
        /// </summary>
        /// <param name="pointResource"></param>
        [HttpPut("api/points")]
        public async Task<IActionResult> UpdatetPointAsync([FromBody] PointResource pointResource)
        {
            var point = PointResource.Map(pointResource);
            await this.pointsService.UpdatePointAsync(point);
            return this.Ok();
        }
        #endregion Public Methods
    }
}