using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using VehicleTracking.Core.Common;
using VehicleTrackingAPI.Common;
using VehicleTrackingAPI.Interfaces;

namespace VehicleTrackingAPI.Implementations
{
    public class MapService : IMapService
    {

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<MapService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public MapService(ILogger<MapService> logger)
        {
            this._logger = logger;
        }

        /// <summary>
        /// Gets the map data asynchronous.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <returns></returns>
        public async Task<MapResponse> GetMapDataAsync(double latitude, double longitude)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var endPoint = $"{Constants.ApiEndpoint}?latlng={latitude},{longitude}&key={Constants.ApiKey}";
                    var response = await client.GetAsync(endPoint);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var mapDto = JsonConvert.DeserializeObject<MapResponse>(responseBody);
                    return mapDto;
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message, exception);
                return null;
            }
        }

        //
        public async Task<string> GetLocality(double latitude, double longitude)
        {
            var mapDto = await GetMapDataAsync(latitude, longitude);

            if (mapDto is null || mapDto.Status.ToLower() != "ok")
                return string.Empty;

            var locality = mapDto.Results.FirstOrDefault()
                .AddressComponents.FirstOrDefault(address => address.Types.Any(addrType => addrType.ToLower() == "locality"))?.LongName;

            return locality;
        }
    }
}
