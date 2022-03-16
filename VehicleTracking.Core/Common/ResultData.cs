using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleTrackingAPI.Common
{
    public class ResultData
    {
        [JsonProperty(PropertyName = "address_components")]
        public List<AddressData> AddressComponents { get; set; } = new List<AddressData>();
    }
}
