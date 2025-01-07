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
            List<UserDto> users = await _dbContext.Set<ApplicationUser>()
                .Select(x => new UserDto(
                    x.Id,
                    x.FirstName + " " + x.LastName
                ))
                .ToListAsync(cancellationToken);

            List<FormLocationDto> locations = await _dbContext.Set<Location>()
                .Select(x => new FormLocationDto(
                    x.Id,
                    x.Name
                ))
                .ToListAsync(cancellationToken);

            List<VehicleDto> vehicles = await _dbContext.Set<Vehicle>()
                .Select(x => new VehicleDto(
                    x.Id,
                    x.VehicleDetails.Vin,
                    x.VehicleDetails.Model
                    ))
                .ToListAsync(cancellationToken);

            return new RoutePlannerFormDto(locations, users, vehicles);
        }
    }
}
