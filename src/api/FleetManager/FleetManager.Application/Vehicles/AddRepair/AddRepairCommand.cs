using FleetManager.Application.Vehicles.Shared;
using FleetManager.Domain.SeedWork.Results;
using FleetManager.Domain.Vehicles;
using MediatR;

namespace FleetManager.Application.Vehicles.AddRepair
{
    public record AddRepairCommand(AddRepairRequest AddRepairRequest) : IRequest<Result>
    {
    }

    internal class AddRepairCommandHandler : IRequestHandler<AddRepairCommand, Result>
    {
        private readonly VehicleService _vehicleService;

        public AddRepairCommandHandler(VehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<Result> Handle(AddRepairCommand request, CancellationToken cancellationToken)
        {
            RepairDto repairDto = request.AddRepairRequest.Repair;

            return await _vehicleService.AddRepairAsync(repairDto.VehicleId, repairDto.Date, repairDto.Description,
                repairDto.Cost, cancellationToken);
        }
    }
}
