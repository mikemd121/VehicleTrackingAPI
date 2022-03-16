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
    public class TrackingDataHelperService : ITrackingDataHelperService
    {
        /// <summary>
        /// Gets the tracking data sender.
        /// </summary>
        public ITrackingDataSender _trackingDataSender { get; }

        /// <summary>
        /// Gets the hd insight connect.
        /// </summary>
        public IHdInsightConnect _hdInsightConnect { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackingDataHelperService"/> class.
        /// </summary>
        /// <param name="trackingDataSender">The tracking data sender.</param>
        /// <param name="hdInsightConnect">The hd insight connect.</param>
        public TrackingDataHelperService(
            ITrackingDataSender trackingDataSender,
            IHdInsightConnect hdInsightConnect)
        {
            _trackingDataSender = trackingDataSender;
            _hdInsightConnect = hdInsightConnect;
        }

        /// <summary>
        /// Sends the data asynchronous.
        /// </summary>
        /// <param name="locationTrackerApiModel">The location tracker API model.</param>
        /// <returns></returns>
        public ResponseModel SendData(LocationTrackerAPIModel locationTrackerApiModel)
        {
            var model = new ResponseModel();
            _trackingDataSender.SendDataAsync(locationTrackerApiModel);
            model.Messsage = "Tracking data Inserted Successfully";
            model.IsSuccess = true;
            return model;
        }

        /// <summary>
        /// Gets the current location asynchronous.
        /// </summary>
        /// <param name="vehicleId">The vehicle identifier.</param>
        /// <returns></returns>
        public TrackingLocationResponse GetCurrentLocationAsync(int vehicleId)
        {
            var sparkQuery = $"select * from TrackingDataTable where VehicleId={vehicleId} order by Id desc LIMIT 1 ";
            var response = _hdInsightConnect.ExecuteSparkSQL(sparkQuery).First();
            return response;
        }
    }
}
