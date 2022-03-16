using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VehicleTrackingAPI.FluentValidator;

namespace VehicleTrackingAPI.Models
{
    public class VehicleAPIModel
    {
        /// <summary>
        /// Image repository.
        /// </summary>
        [Display(Name = "Vehicle identification number")]
        public int VehicleId { get; set; }

        public string VehicleRegistrationNo { get; set; }
        /// <summary>
        /// Image tag.
        /// </summary>
        [Display(Name = "Device sensor code")]
        public string DeviceSensorCode { get; set; }
    }
}
