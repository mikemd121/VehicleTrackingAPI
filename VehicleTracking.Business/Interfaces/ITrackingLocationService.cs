using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTrackingAPI.Common;
using VehicleTrackingAPI.Models;
using VehicleTrackingAPI.ViewModels;

namespace VehicleTrackingAPI.Interfaces
{
    public interface ITrackingLocationService
    {
        Task<ResponseModel> AddLocationAsync(LocationTrackerAPIModel locationTrackerAPIModel);

        TrackingLocationResponse GetCurrentLocationAsync(int vehicleId);

        List<TrackingLocationResponse> GetLocationList(int vehicleId, DateTime fromDateTime, DateTime toDateTime);

    }
}
