using FleetManager.Application.Vehicles.Shared;
using FleetManager.Domain.SeedWork.Results;
using FleetManager.Domain.Vehicles;
using MediatR;

namespace FleetManager.Application.Vehicles.AddInspection
{
    public record AddInspectionCommand(AddInspectionRequest AddInspectionRequest) : IRequest<Result>
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
            InspectionDto inspectionDto = request.AddInspectionRequest.InspectionDto;

            return await _vehicleService.AddInspectionAsync(inspectionDto.VehicleId, inspectionDto.Date, inspectionDto.Description,
                inspectionDto.Cost, cancellationToken);
        }
    }
}
