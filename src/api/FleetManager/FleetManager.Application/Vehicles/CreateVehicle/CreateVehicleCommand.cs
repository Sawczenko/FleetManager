using FleetManager.Domain.SeedWork.Results;
using FleetManager.Domain.Locations;
using FleetManager.Domain.Vehicles;
using MediatR;

namespace FleetManager.Application.Vehicles.CreateVehicle
{
    public record CreateVehicleCommand(CreateVehicleRequest Request) : IRequest<Result>
    {
    }

    internal class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand,  Result>
    {
        private readonly VehicleService _vehicleService;

        public CreateVehicleCommandHandler(VehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<Result> Handle(CreateVehicleCommand command, CancellationToken cancellationToken)
        {
            CreateVehicleRequest request = command.Request;
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
