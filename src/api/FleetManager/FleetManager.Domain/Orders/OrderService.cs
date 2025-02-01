using FleetManager.Domain.SeedWork.Results;
using FleetManager.Domain.SeedWork;

namespace FleetManager.Domain.Orders
{
    public class OrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IOrderRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Order>> CreateNewOrderAsync(
            Guid contractorId,
            Guid originId,
            Guid destinationId,
            DateTime pickupDate,
            DateTime deliveryDate,
            CancellationToken cancellationToken)
        {
            Result<Order> result = OrderFactory.Create(contractorId, originId, destinationId, pickupDate, deliveryDate);

            if (result.IsFailure)
            {
                return result;
            }

            await _repository.AddAsync(result.Value, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return result;
        }
    }
}
