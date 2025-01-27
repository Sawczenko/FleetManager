using FleetManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using MediatR;
using FleetManager.Domain.Itineraries;

namespace FleetManager.Application.Itineraries.GetTodaysItinerariesCountPerStatus;

public class GetTodaysItinerariesCountPerStatusQuery : IRequest<Dictionary<ItineraryStatus, int>>
{

}

internal class GetTodaysRouteCountPerStatusQueryHandler : IRequestHandler<GetTodaysItinerariesCountPerStatusQuery, Dictionary<ItineraryStatus, int>>
{
    private readonly FleetManagerDbContext _dbContext;

    public GetTodaysRouteCountPerStatusQueryHandler(FleetManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Dictionary<ItineraryStatus, int>> Handle(GetTodaysItinerariesCountPerStatusQuery request, CancellationToken cancellationToken)
    {
        DateTime currentDate = DateTime.UtcNow;

        return Task.FromResult(new Dictionary<ItineraryStatus, int>());

        //return _dbContext.Set<Itinerary>()
        //    .Where(x => x.ScheduledStartDate <= currentDate)
        //    .Where(x => x.ActualEndTime == null || EF.Functions.DateDiffDay(x.ActualEndTime, currentDate) == 0)
        //    .GroupBy(x => x.Status)
        //    .Select(x => new
        //    {
        //        Status = x.Key,
        //        Count = x.Count(),
        //    })
        //    .ToDictionaryAsync(x => x.Status, x => x.Count, cancellationToken);
    }
}