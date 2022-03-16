using Microsoft.Extensions.DependencyInjection;
using VehicleTracking.Business.Implementations;
using VehicleTracking.Business.Interfaces;
using VehicleTrackingAPI.Implementations;
using VehicleTrackingAPI.Interfaces;

namespace VehicleTrackingAPI.ServicesExtensions
{
    public static class ServicesExtensions
    {
        /// <summary>
        /// Adds my library services.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void AddMyLibraryServices(this IServiceCollection services)
        {
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<ITrackingLocationService, TrackingLocationService>();
            services.AddScoped<IMapService, MapService>();
            services.AddScoped<ITrackingDataSender, TrackingDataSender>();
            services.AddScoped<IHdInsightConnect, HdInsightConnectService>();
            services.AddScoped<ITrackingDataHelperService, TrackingDataHelperService>();
            services.AddScoped<ILoginService, LoginService>();
        }
    }
}
