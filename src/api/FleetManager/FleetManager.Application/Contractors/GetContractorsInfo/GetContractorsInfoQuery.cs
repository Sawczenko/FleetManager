using FleetManager.Domain.Contractors;
using FleetManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace FleetManager.Application.Contractors.GetContractorsInfo;

public record GetContractorsInfoQuery : IRequest<List<ContractorInfo>>
{
    
}

internal class GetContractorsInfoQueryHandler : IRequestHandler<GetContractorsInfoQuery, List<ContractorInfo>>
{
    private readonly FleetManagerDbContext _dbContext;

    public GetContractorsInfoQueryHandler(FleetManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<ContractorInfo>> Handle(GetContractorsInfoQuery request, CancellationToken cancellationToken)
    {
        return _dbContext.Set<Contractor>().Select(contractor => new ContractorInfo(contractor.Id, contractor.Name))
            .ToListAsync(cancellationToken);
    }
}