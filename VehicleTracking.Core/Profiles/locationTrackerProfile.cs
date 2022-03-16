using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTrackingAPI.Common;
using VehicleTrackingAPI.Models;

namespace VehicleTrackingAPI.Profiles
{
    public class locationTrackerProfile :Profile
    {
        public locationTrackerProfile()
        {
            CreateMap<LocationTrackerAPIModel, LocationTracker>().ForMember(dest => dest.CreatedDateTime, opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<LocationTracker, TrackingLocationResponse>();
        }
    }
}
