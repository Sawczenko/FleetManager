using FleetManager.Domain.SeedWork.Results;
using FleetManager.Domain.Vehicles.Models;
using FleetManager.Domain.Locations;
using FleetManager.Domain.SeedWork;

namespace FleetManager.Domain.Vehicles
{
    public sealed class VehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public VehicleService(IVehicleRepository vehicleRepository, IUnitOfWork unitOfWork)
        {
            _vehicleRepository = vehicleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> AddNewVehicleAsync(string vin,
            string licensePlate,
            string model,
            DateTime lastInspectionDate,
            DateTime nextInspectionDate,
            Location? currentLocation,
            CancellationToken cancellationToken = new())
        {
            var result = VehicleFactory.Create(vin, licensePlate, model, lastInspectionDate, nextInspectionDate,
                currentLocation);

            if (result.IsFailure)
            {
                return result;
            }

            if (result.Value is null)
            {
                return Result.Failure(VehicleErrors.VehicleWasNotCreated);
            }

            Vehicle newVehicle = result.Value;

            Vehicle? existingVehicle =
                await _vehicleRepository.GetByVehicleDetailsAsync(newVehicle.VehicleDetails, cancellationToken);

            if (existingVehicle is not null)
            {
                return Result.Failure(VehicleErrors.VehicleAlreadyExists(existingVehicle.VehicleDetails));
            }

            await _vehicleRepository.AddAsync(newVehicle, cancellationToken);

            return Result.Success();
        }

        public async Task<Result> AddRepairAsync(Guid vehicleId, DateTime date, string? description, double cost, CancellationToken cancellationToken)
        {
            if (date == default || string.IsNullOrWhiteSpace(description) || cost == 0 || vehicleId == Guid.Empty)
            {
                return Result.Failure(VehicleErrors.VehicleWasNotCreated);
            }

            Vehicle? vehicle = await _vehicleRepository.GetByIdAsync(vehicleId, cancellationToken);

            if (vehicle is null)
            {
                return Result.Failure(VehicleErrors.VehicleWasNotCreated);
            }

            vehicle.AddRepair(new Repair(vehicleId, date, description, cost));

            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Success();
        }

        public async Task<Result> AddInspectionAsync(Guid vehicleId, DateTime date, string description, double cost, CancellationToken cancellationToken)
        {
            if (date == default || string.IsNullOrWhiteSpace(description) || cost == 0 || vehicleId == Guid.Empty)
            {
                return Result.Failure(VehicleErrors.VehicleWasNotCreated);
            }

            Vehicle? vehicle = await _vehicleRepository.GetByIdAsync(vehicleId, cancellationToken);

            if (vehicle is null)
            {
                return Result.Failure(VehicleErrors.VehicleWasNotCreated);
            }

            vehicle.AddInspection(new Inspection(vehicleId, date, description, cost));

            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Success();
        }
    }
}
