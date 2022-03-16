using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTrackingAPI.Models;
using VehicleTrackingAPI.ViewModels;

namespace VehicleTrackingAPI.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IVehicleService
    {
        /// <summary>
        /// Registers the vehicle.
        /// </summary>
        /// <param name="vehicleAPIModel">The vehicle API model.</param>
        /// <returns></returns>
        ResponseModel RegisterVehicle(VehicleAPIModel vehicleAPIModel);

        /// <summary>
        /// Determines whether [is vehicle registered] [the specified vehicle identifier].
        /// </summary>
        /// <param name="vehicleId">The vehicle identifier.</param>
        bool IsVehicleRegistered(int vehicleId);
    }
}
