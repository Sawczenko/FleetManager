using FleetManager.Application.Routes.GetRoutePlannerForm.Dto;
using FleetManager.Infrastructure.Authentication;
using FleetManager.Domain.Vehicles.Models;
using FleetManager.Infrastructure.Data;
using FleetManager.Domain.Locations;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace FleetManager.Application.Routes.GetRoutePlannerForm
{
    public record GetRoutePlannerFormQuery : IRequest<RoutePlannerFormDto>
    {
    }

    internal class GetRoutePlannerFormQueryHandler : IRequestHandler<GetRoutePlannerFormQuery, RoutePlannerFormDto>
    {
        private readonly FleetManagerDbContext _dbContext;

        public GetRoutePlannerFormQueryHandler(FleetManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<RoutePlannerFormDto> Handle(GetRoutePlannerFormQuery request, CancellationToken cancellationToken)
        {
            List<FormLocationDto> locations = await _dbContext.Set<Location>()
                .Select(x => new FormLocationDto(
                    x.Id,
                    x.Name
                ))
                .ToListAsync(cancellationToken);

            return new RoutePlannerFormDto(locations);
        }
    }
}
