using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTrackingAPI.Common;

namespace VehicleTrackingAPI.Interfaces
{
    public interface IHdInsightConnect
    {
        List<TrackingLocationResponse> ExecuteSparkSQL(string sparkQuery);
    }
}
