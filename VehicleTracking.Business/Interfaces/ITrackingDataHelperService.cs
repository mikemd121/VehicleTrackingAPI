using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTrackingAPI.Common;
using VehicleTrackingAPI.Models;
using VehicleTrackingAPI.ViewModels;

namespace VehicleTrackingAPI.Interfaces
{
    public interface ITrackingDataHelperService
    {
        ResponseModel SendData(LocationTrackerAPIModel locationTrackerAPIModel);

        TrackingLocationResponse GetCurrentLocationAsync(int vehicleId);
    }
}
