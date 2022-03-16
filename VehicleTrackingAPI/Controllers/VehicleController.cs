using Microsoft.AspNetCore.Mvc;
using System;
using VehicleTrackingAPI.Interfaces;
using VehicleTrackingAPI.Models;

namespace VehicleTrackingAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        /// <summary>
        /// The vehicle service
        /// </summary>
        IVehicleService _vehicleService;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleController"/> class.
        /// </summary>
        /// <param name="vehicleService">The vehicle service.</param>
        public VehicleController(IVehicleService  vehicleService)
        {
            _vehicleService = vehicleService;
        }

        /// <summary>
        /// Saves the vehicle.
        /// </summary>
        /// <param name="vehicle">The vehicle.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("", Name = "")]
        public IActionResult RegisterVehicle(VehicleAPIModel vehicle)
        {
            try
            {
                var model = _vehicleService.RegisterVehicle(vehicle);
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
