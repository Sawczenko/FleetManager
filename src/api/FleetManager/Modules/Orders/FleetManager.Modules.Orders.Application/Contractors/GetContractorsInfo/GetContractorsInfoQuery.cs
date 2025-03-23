using Dapper;
using FleetManager.BuildingBlocks.Application.Data;
using FleetManager.Modules.Orders.Domain.Contractors;
using MediatR;

namespace FleetManager.Modules.Orders.Application.Contractors.GetContractorsInfo;

public record GetContractorsInfoQuery : IRequest<List<ContractorInfo>>
{
    
}

internal class GetContractorsInfoQueryHandler : IRequestHandler<GetContractorsInfoQuery, List<ContractorInfo>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;


    public GetContractorsInfoQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<List<ContractorInfo>> Handle(GetContractorsInfoQuery request, CancellationToken cancellationToken)
    {
        var connection = _sqlConnectionFactory.GetOpenConnection();
        
        const string sql = $"""
                            SELECT
                                [Contractors].[Id] AS [{nameof(Contractor.Id)}],
                                [Contractors].[Name] AS [{nameof(Contractor.Name)}],
                            FROM [orders].[Contractors] AS [Contractors]
                            """;
        
        var contractorInfos = await connection.QueryAsync<ContractorInfo>(sql);
        
        return contractorInfos.AsList();
    }
}