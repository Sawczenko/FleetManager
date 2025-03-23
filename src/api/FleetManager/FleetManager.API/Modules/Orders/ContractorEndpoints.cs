using FleetManager.BuildingBlocks.Domain.Results;
using FleetManager.Modules.Orders.Application.Contractors.GetContractorsInfo;
using FleetManager.Modules.Orders.Domain.Contractors;
using MediatR;

namespace FleetManager.API.Modules.Orders;

public static class ContractorEndpoints
{
    public static void MapContractorEndpoints(this IEndpointRouteBuilder app)
    {
        // var group = app.MapGroup("contractors").WithTags("Contractors");
        //
        // group.MapGet("", async (IMediator mediator,CancellationToken cancellationToken) =>
        // {
        //     var contractorsInfo = await mediator.Send(new GetContractorsInfoQuery(), cancellationToken);
        //     
        //     return Results.Ok(Result<List<ContractorInfo>>.Success(contractorsInfo));
        // });
    }
}