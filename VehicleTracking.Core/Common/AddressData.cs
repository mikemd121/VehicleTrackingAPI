using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleTrackingAPI.Common
{
    public class AddressData
    {
        [JsonProperty(PropertyName = "types")]
        public List<string> Types { get; set; } = new List<string>();

        [JsonProperty(PropertyName = "short_name")]
        public string ShortName { get; set; }

        [JsonProperty(PropertyName = "long_name")]
        public string LongName { get; set; }
    }
}
