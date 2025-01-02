using FleetManager.Application.Vehicles.Shared;
using FleetManager.Domain.SeedWork.Results;
using FleetManager.Domain.Vehicles;
using MediatR;

namespace FleetManager.Application.Vehicles.AddInspection
{
    public record AddInspectionCommand(InspectionDto InspectionDto) : IRequest<Result>
    {
    }

    internal class AddInspectionCommandHandler : IRequestHandler<AddInspectionCommand, Result>
    {
        private readonly VehicleService _vehicleService;

        public AddInspectionCommandHandler(VehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<Result> Handle(AddInspectionCommand request, CancellationToken cancellationToken)
        {
            InspectionDto inspectionDto = request.InspectionDto;

            return await _vehicleService.AddInspectionAsync(Guid.Parse(inspectionDto.VehicleId), inspectionDto.Date, inspectionDto.Description,
                inspectionDto.Cost, cancellationToken);
        }
    }
}
