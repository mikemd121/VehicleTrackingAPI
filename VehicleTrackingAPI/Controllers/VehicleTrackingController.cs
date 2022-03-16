using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using VehicleTrackingAPI.Common;
using VehicleTrackingAPI.Interfaces;
using VehicleTrackingAPI.Models;

namespace VehicleTrackingAPI.Controllers
{

    /// <summary>
    /// Vehicle tracking controller.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/v1/[controller]")]
    [ApiController]
    public class VehicleTrackingController : ControllerBase
    {
        /// <summary>
        /// The tracking location service
        /// </summary>
        public readonly ITrackingLocationService TrackingLocationService;

        /// <summary>
        /// The vehicle service
        /// </summary>
        public readonly IVehicleService VehicleService;

        /// <summary>
        /// The tracking data helper service
        /// </summary>
        private readonly ITrackingDataHelperService _trackingDataHelperService;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleTrackingController"/> class.
        /// </summary>
        /// <param name="trackingLocationService">The tracking location service.</param>
        /// <param name="vehicleService">The vehicle service.</param>
        /// <param name="trackingDataHelperService">The tracking data helper service.</param>
        public VehicleTrackingController(
            ITrackingLocationService trackingLocationService,
            IVehicleService vehicleService,
            ITrackingDataHelperService trackingDataHelperService)
        {
            this.TrackingLocationService = trackingLocationService;
            this.VehicleService = vehicleService;
            this._trackingDataHelperService = trackingDataHelperService;
        }

        /// <summary>
        /// Adds the location asynchronous.
        /// </summary>
        /// <param name="locationTrackerApiModel">The location tracker API model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("addlocationtrackdata")]
        public IActionResult AddLocationAsync(LocationTrackerAPIModel locationTrackerApiModel)
        {
            var model = TrackingLocationService.AddLocationAsync(locationTrackerApiModel);
            return Ok(model);
        }

        /// <summary>
        /// Gets the current location asynchronous.
        /// </summary>
        /// <param name="vehicleId">The vehicle identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{vehicleId}/current-location")]
        [Authorize(Policy = "Admin")]
        [ProducesResponseType(typeof(TrackingLocationResponse), (int)HttpStatusCode.OK)]
        public ActionResult<TrackingLocationResponse> GetCurrentLocationAsync(int vehicleId)
        {
            if (!VehicleService.IsVehicleRegistered(vehicleId))
                return BadRequest(new Response { IsSuccess = false, Message = "Vehicle is not registered." });
            var model = TrackingLocationService.GetCurrentLocationAsync(vehicleId);
            return Ok(model);
        }

        /// <summary>
        /// Gets the location list.
        /// </summary>
        /// <param name="vehicleId">The vehicle identifier.</param>
        /// <param name="fromDateTime">From date time.</param>
        /// <param name="toDateTime">To date time.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{vehicleId}/{fromDateTime}/{toDateTime}/locationdata")]
        [Authorize(Policy = "Admin")]
        [ProducesResponseType(typeof(List<TrackingLocationResponse>), (int)HttpStatusCode.OK)]
        public ActionResult<List<TrackingLocationResponse>> GetLocationList(int vehicleId, DateTime fromDateTime, DateTime toDateTime)
        {

            if (!VehicleService.IsVehicleRegistered(vehicleId))
                return BadRequest(new Response { IsSuccess = false, Message = "Vehicle is not registered." });
            var locationList = TrackingLocationService.GetLocationList(vehicleId, fromDateTime, toDateTime);
            return Ok(locationList);

        }

        /// <summary>
        /// Adds the tracking data through azure event hub.
        /// </summary>
        /// <param name="locationTrackerApiModel">The location tracker API model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("posttrackingdata-eventhub")]
        [Authorize(Policy = "Admin")]
        public IActionResult AddTrackingDataThroughAzureEventHub(LocationTrackerAPIModel locationTrackerApiModel)
        {
            var model = _trackingDataHelperService.SendData(locationTrackerApiModel);
            return Ok(model);
        }

        /// <summary>
        /// Gets the current location by event hub asynchronous.
        /// </summary>
        /// <param name="vehicleId">The vehicle identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{vehicleId}/getcurrent-location-eventhub")]
        [ProducesResponseType(typeof(TrackingLocationResponse), (int)HttpStatusCode.OK)]
        [Authorize(Policy = "Admin")]
        public ActionResult<TrackingLocationResponse> GetCurrentLocationByEventHubAsync(int vehicleId)
        {
            var model = _trackingDataHelperService.GetCurrentLocationAsync(vehicleId);
            return Ok(model);
        }
    }
}
