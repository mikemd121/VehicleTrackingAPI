using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTrackingAPI.Common;

namespace VehicleTrackingAPI.Interfaces
{
   public interface IMapService
    {
        Task<MapResponse> GetMapDataAsync(double latitude, double longitude);
        Task<string> GetLocality(double latitude, double longitude);
    }
}
