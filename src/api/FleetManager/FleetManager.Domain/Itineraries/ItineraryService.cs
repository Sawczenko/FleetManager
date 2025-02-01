using FleetManager.Domain.SeedWork.Results;
using FleetManager.Domain.SeedWork;

namespace FleetManager.Domain.Itineraries;

public class ItineraryService
{
    private readonly IItineraryRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public ItineraryService(IItineraryRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> CreateNew(
        List<OrderRouting> routedOrders,
        Guid driverId, 
        Guid vehicleId,
        DateTime startTime,
        DateTime endTime,
        CancellationToken cancellationToken)
    {
        Result<Itinerary> result = ItineraryFactory.Create(routedOrders, driverId, vehicleId, startTime, endTime);

        if (result.IsFailure)
        {
            return result;
        }

        await _repository.AddAsync(result.Value, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Success();
    }
}