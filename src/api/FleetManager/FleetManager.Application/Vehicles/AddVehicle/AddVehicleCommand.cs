using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FleetManager.Domain.Locations;
using FleetManager.Domain.SeedWork.Results;
using FleetManager.Domain.Vehicles;
using MediatR;

namespace FleetManager.Application.Vehicles.AddVehicle
{
    public record AddVehicleCommand(AddVehicleRequest Request) : IRequest<Result>
    {
    }

    internal class AddVehicleCommandHandler : IRequestHandler<AddVehicleCommand,  Result>
    {
        private readonly VehicleService _vehicleService;

        public AddVehicleCommandHandler(VehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<Result> Handle(AddVehicleCommand command, CancellationToken cancellationToken)
        {
            AddVehicleRequest request = command.Request;
            return await _vehicleService.AddNewVehicleAsync(
                request.Vin,
                request.LicensePlate,
                request.Model,
                request.LastInspectionDate,
                request.NextInspectionDate,
                new Location(request.LocationName, request.Latitude, request.Longitude),
                cancellationToken
                );
        }
    }
}
