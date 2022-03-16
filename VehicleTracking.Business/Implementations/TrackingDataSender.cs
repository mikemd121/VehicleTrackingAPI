using AutoMapper;
using Microsoft.Azure.EventHubs;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Core.Common;
using VehicleTrackingAPI.Interfaces;
using VehicleTrackingAPI.Models;

namespace VehicleTrackingAPI.Implementations
{
    public class TrackingDataSender : ITrackingDataSender
    {
        /// <summary>
        /// The connection string
        /// </summary>
        public static string ConnectionString = Constants.AzureEventHubConnection;

        /// <summary>
        /// The event hub client
        /// </summary>
        private readonly EventHubClient _eventHubClient;

        /// <summary>
        /// Gets the mapper.
        /// </summary>
        public IMapper _mapper { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackingDataSender"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        public TrackingDataSender(IMapper mapper)
        {
            _eventHubClient = EventHubClient.CreateFromConnectionString(ConnectionString);
            _mapper = mapper;
        }

        /// <summary>
        /// Sends the data asynchronous.
        /// </summary>
        /// <param name="locationTrackerApiModel">The location tracker API model.</param>
        public async Task SendDataAsync(LocationTrackerAPIModel locationTrackerApiModel)
        {
            var eventData = CreateEventData(locationTrackerApiModel);
            await _eventHubClient.SendAsync(eventData);
        }

        /// <summary>
        /// Creates the event data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        private EventData CreateEventData(LocationTrackerAPIModel data)
        {
            var locationTrackerObj = _mapper.Map<LocationTracker>(data);
            var dataAsJson = JsonConvert.SerializeObject(locationTrackerObj);
            var eventData = new EventData(Encoding.UTF8.GetBytes(dataAsJson));
            return eventData;
        }
    }
}
