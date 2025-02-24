using FleetManager.Application.Contractors.GetContractorsInfo;
using FleetManager.Application.Locations.GetLocationsInfo;
using FleetManager.Application.Orders.GetOrders;
using MediatR;

namespace FleetManager.Application.Orders.GetOrdersManagementFilter
{
    public record GetOrdersManagementFilterQuery : IRequest<OrderManagementFilterDto>
    {
    }

    internal class GetOrdersManagementFilterQueryHandler : IRequestHandler<GetOrdersManagementFilterQuery, OrderManagementFilterDto>
    {
        private readonly IMediator _mediator;

        public GetOrdersManagementFilterQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OrderManagementFilterDto> Handle(GetOrdersManagementFilterQuery request, CancellationToken cancellationToken)
        {
            var contractors = await _mediator.Send(new GetContractorsInfoQuery(), cancellationToken);

            var locations = await _mediator.Send(new GetLocationsInfoQuery(), cancellationToken);

            return new OrderManagementFilterDto(locations, contractors);
        }
    }
}
