using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTrackingAPI.Common;
using VehicleTrackingAPI.Interfaces;
using VehicleTrackingAPI.Models;
using VehicleTrackingAPI.ViewModels;

namespace VehicleTrackingAPI.Implementations
{
    public class TrackingLocationService : ITrackingLocationService
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly VehicleTrackContext _context;

        /// <summary>
        /// The mapper.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// The vehicle service
        /// </summary>
        private readonly IVehicleService _vehicleService;

        /// <summary>
        /// The map service
        /// </summary>
        private readonly IMapService _mapService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackingLocationService"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="vehicleService">The vehicle service.</param>
        /// <param name="mapService">The map service.</param>
        public TrackingLocationService(
            VehicleTrackContext context,
            IMapper mapper,
            IVehicleService vehicleService,
            IMapService mapService)
        {
            _context = context;
            this._mapper = mapper;
            this._vehicleService = vehicleService;
            this._mapService = mapService;
        }

        /// <summary>
        /// Adds the location asynchronous.
        /// </summary>
        /// <param name="locationTrackerApiModel">The location tracker API model.</param>
        /// <returns></returns>
        public async Task<ResponseModel> AddLocationAsync(LocationTrackerAPIModel locationTrackerApiModel)
        {
            var model = new ResponseModel();
            var temp = _vehicleService.IsVehicleRegistered(locationTrackerApiModel.VehicleId);

            if (!temp)
            {
                model.Messsage = "Vehicle is not registered for the device.";
                model.IsSuccess = false;
                return model;
            }

            var locationTrackerObj = _mapper.Map<LocationTracker>(locationTrackerApiModel);
            await _context.AddAsync(locationTrackerObj);
            model.Messsage = "Tracking data Inserted Successfully";
            _context.SaveChanges();
            model.IsSuccess = true;
            return model;
        }



        /// <summary>
        /// Gets the location list.
        /// </summary>
        /// <param name="vehicleId">The vehicle identifier.</param>
        /// <param name="fromDateTime">From date time.</param>
        /// <param name="toDateTime">To date time.</param>
        /// <returns></returns>
        public List<TrackingLocationResponse> GetLocationList(int vehicleId, DateTime fromDateTime, DateTime toDateTime)
        {
            var locationList = GetLocationListByParameters(vehicleId, fromDateTime, toDateTime);
            return locationList;
        }


        /// <summary>
        /// Gets the location list by parameters.
        /// </summary>
        /// <param name="vehicleId">The vehicle identifier.</param>
        /// <param name="fromDateTime">From date time.</param>
        /// <param name="toDateTime">To date time.</param>
        /// <returns></returns>
        private List<TrackingLocationResponse> GetLocationListByParameters(int vehicleId, DateTime fromDateTime, DateTime toDateTime)
        {
            var trackedLocationList = _context.LocationTrackers
                  .Where(b => b.VehicleId == vehicleId && (b.CreatedDateTime >= fromDateTime && b.CreatedDateTime <= toDateTime)).ToList();
            if (!trackedLocationList.Any()) throw new Exception("Location contains no elements");
            var locationList = _mapper.Map<List<TrackingLocationResponse>>(trackedLocationList);
            return locationList;
        }

        /// <summary>
        /// Gets the current location asynchronous.
        /// </summary>
        /// <param name="vehicleId">The vehicle identifier.</param>
        /// <returns></returns>
        public TrackingLocationResponse GetCurrentLocationAsync(int vehicleId)
        {
            var location = _context.LocationTrackers
                  .Where(b => b.VehicleId == vehicleId).OrderByDescending(x => x.Id).FirstOrDefault();
            var mappedLocation = _mapper.Map<TrackingLocationResponse>(location);
            var locality = _mapService.GetLocality(mappedLocation.Latitude, mappedLocation.Longitude);
            mappedLocation.LocalityPositionData = locality.Result;
            return mappedLocation;
        }
    }
}
