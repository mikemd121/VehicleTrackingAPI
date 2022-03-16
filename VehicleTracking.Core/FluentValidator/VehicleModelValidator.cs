using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTrackingAPI.Models;

namespace VehicleTrackingAPI.FluentValidator
{
    public class VehicleModelValidator : AbstractValidator<VehicleAPIModel>
    {
        public VehicleModelValidator()
        {
            RuleFor(validator => validator.VehicleId).NotNull().NotEmpty().WithMessage("Vehicle id cannot be empty.");
            RuleFor(validator => validator.VehicleId).GreaterThanOrEqualTo(0).WithMessage("Vehicle id cannot be 0 or less than 0.");
            RuleFor(validator => validator.DeviceSensorCode).NotNull().NotEmpty().WithMessage("Device sensor code cannot be empty.");
            RuleFor(validator => validator.VehicleRegistrationNo).NotNull().NotEmpty().WithMessage("Vehicle registration no cannot be empty.");
        }

    }
}
