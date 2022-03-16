using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using VehicleTrackingAPI.Common;
using VehicleTrackingAPI.Interfaces;

namespace VehicleTrackingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapsController : ControllerBase
    {
        private readonly IMapService _mapService;
        public MapsController(IMapService mapService)
        {
            this._mapService = mapService;
        }

        [HttpGet()]
        [ProducesResponseType(typeof(MapResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<MapResponse>> GetMapDataAsync([FromQuery][Required] double latitude, [FromQuery][Required] double longitude)
        {
            var mapDto = await _mapService.GetMapDataAsync(latitude, longitude);
            return Ok(mapDto);
        }
    }
}
