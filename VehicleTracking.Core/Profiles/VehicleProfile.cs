using AutoMapper;
using System;
using VehicleTrackingAPI.Models;

namespace VehicleTrackingAPI.Profiles
{
    public class VehicleProfile : Profile
    {
        public VehicleProfile()
        {
            CreateMap<VehicleAPIModel, Vehicle>().ForMember(dest => dest.CreatedDateTime, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}
