using FleetManager.Domain.Aggregates.Locations;
using FleetManager.Domain.SeedWork;

namespace FleetManager.Domain.Aggregates.Vehicles
{
    public sealed class VehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<Result> AddNewVehicleAsync(string vin,
            string licensePlate,
            string model,
            DateTime lastInspectionDate,
            DateTime nextInspectionDate,
            Location? currentLocation,
            List<Inspection>? inspections = null,
            List<Repair>? repairs = null,
            CancellationToken cancellationToken = new())
        {
            if (string.IsNullOrWhiteSpace(vin))
            {
                throw new ArgumentException("VIN cannot be empty.", nameof(vin));
            }

            if (string.IsNullOrWhiteSpace(licensePlate))
            {
                throw new ArgumentException("License plate cannot be empty.", nameof(licensePlate));
            }

            if (string.IsNullOrWhiteSpace(model))
            {
                throw new ArgumentException("Model cannot be empty.", nameof(model));
            }

            if (currentLocation is null)
            {
                throw new ArgumentException("Initial location ID is required.", nameof(currentLocation));

            }

            if (lastInspectionDate > DateTime.UtcNow)
            {
                throw new ArgumentException("Last inspection date cannot be in the future.");
            }

            if (nextInspectionDate < DateTime.UtcNow)
            {
                throw new ArgumentException("Next inspection date cannot be in the past.");
            }

            if (nextInspectionDate < lastInspectionDate)
            {
                throw new ArgumentException("Next inspection date cannot be older than last inspection date.");
            }

            Vehicle vehicle = new Vehicle(vin,
                licensePlate, 
                model,
                lastInspectionDate, 
                nextInspectionDate,
                currentLocation,
                inspections,
                repairs);

            await _vehicleRepository.AddAsync(vehicle, cancellationToken);

            return Result.Success();
        }
    }
}
