using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTrackingAPI.Interfaces;
using VehicleTrackingAPI.Models;
using VehicleTrackingAPI.ViewModels;

namespace VehicleTrackingAPI.Implementations
{
    public class VehicleService : IVehicleService
    {
        /// <summary>
        /// The context
        /// </summary>
        private VehicleTrackContext _context;

        /// <summary>
        /// The mapper.
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleService"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="mapper">The mapper.</param>
        public VehicleService(
            VehicleTrackContext context,
            IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }


        /// <summary>
        /// Registers the vehicle.
        /// </summary>
        /// <param name="vehicleModel">The vehicle model.</param>
        /// <returns></returns>
        public ResponseModel RegisterVehicle(VehicleAPIModel vehicleModel)
        {
            var model = new ResponseModel();
            try
            {
                var _temp = IsVehicleRegistered(vehicleModel.VehicleId);
                if (_temp)
                {
                    model.Messsage = "Vehicle already registered for the device.";
                    model.IsSuccess = false;
                }
                else
                {
                    var vehicle = mapper.Map<Vehicle>(vehicleModel);
                    _context.Add<Vehicle>(vehicle);
                    model.IsSuccess = true;
                    model.Messsage = "Vehicle registration for the device completed.";
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Messsage = "Error : " + ex.Message;
            }
            return model;
        }

        /// <summary>
        /// Determines whether [is vehicle registered] [the specified identifier].
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <param name="vehicleId">The vehicle identifier.</param>
        /// <returns>
        ///   <c>true</c> if [is vehicle registered] [the specified identifier]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsVehicleRegistered(int vehicleId)
        {
            var isExist = _context.Vehicles
           .Where(b => b.VehicleId == vehicleId)
           .Any();

            if (isExist)
                return true;
            return false;
        }

    }
}
