using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTrackingAPI.Models;

namespace VehicleTrackingAPI.Interfaces
{
   public interface ITrackingDataSender
    {
        Task SendDataAsync(LocationTrackerAPIModel locationTrackerAPIModel);
    }
}
