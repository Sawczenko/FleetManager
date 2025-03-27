using Dapper;
using FleetManager.BuildingBlocks.Application.Data;
using FleetManager.Modules.Itineraries.Domain;
using MediatR;

namespace FleetManager.Modules.Itineraries.Application.Itineraries.GetTodaysItinerariesCountPerStatus;

public class GetTodaysItinerariesCountPerStatusQuery : IRequest<Dictionary<ItineraryStatus, int>>
{
}

internal class GetTodaysRouteCountPerStatusQueryHandler : IRequestHandler<GetTodaysItinerariesCountPerStatusQuery,
    Dictionary<ItineraryStatus, int>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetTodaysRouteCountPerStatusQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Dictionary<ItineraryStatus, int>> Handle(GetTodaysItinerariesCountPerStatusQuery request,
        CancellationToken cancellationToken)
    {
        string sql = $"""
                      SELECT
                        [Itineraries].[Status] AS [{nameof(Itinerary.Status)}],
                        COUNT(*) AS [Count]
                      FROM [itineraries].[Itineraries] AS [Itineraries]
                      WHERE [Itineraries].[ScheduledStartDate] >= @currentDate
                      AND ([Itineraries].[ActualEndTime] IS NULL AND DATEDIFF(DAY, [Itineraries].[ActualEndTime], @currentDate) = 0))
                      GROUP BY [Itineraries].[Status]
                      """;
        
        var connection = _sqlConnectionFactory.GetOpenConnection();
        
        var result = await connection.QueryAsync<(int Status, int Count)>(sql, new { CurrentDate = DateTime.UtcNow });

        var itinerariesCountByStatus = result.ToDictionary(x => (ItineraryStatus)x.Status, x => x.Count);
        
        return itinerariesCountByStatus;
    }
}