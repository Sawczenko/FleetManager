using FleetManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using FleetManager.Domain.Routes;
using MediatR;

namespace FleetManager.Application.Routes.GetTodaysRoutesCountPerStatus;

public class GetTodaysRouteCountPerStatusQuery : IRequest<Dictionary<RouteStatus, int>>
{

}

internal class GetTodaysRouteCountPerStatusQueryHandler : IRequestHandler<GetTodaysRouteCountPerStatusQuery, Dictionary<RouteStatus, int>>
{
    private readonly FleetManagerDbContext _dbContext;

    public GetTodaysRouteCountPerStatusQueryHandler(FleetManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Dictionary<RouteStatus, int>> Handle(GetTodaysRouteCountPerStatusQuery request, CancellationToken cancellationToken)
    {
        DateTime currentDate = DateTime.UtcNow;

        return _dbContext.Set<Route>()
            .Where(x => x.ScheduledStartTime <= currentDate)
            .Where(x => x.ActualEndTime == null || EF.Functions.DateDiffDay(x.ActualEndTime, currentDate) == 0)
            .GroupBy(x => x.Status)
            .Select(x => new
            {
                Status = x.Key,
                Count = x.Count(),
            })
            .ToDictionaryAsync(x => x.Status, x => x.Count, cancellationToken);
    }
}