using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleTrackingAPI.Models
{
    public class Vehicle : BaseEntity
    {
        public int VehicleId { get; set; }

        public string VehicleRegistrationNo { get; set; }
        public string DeviceSensorCode { get; set; }

    }
}
