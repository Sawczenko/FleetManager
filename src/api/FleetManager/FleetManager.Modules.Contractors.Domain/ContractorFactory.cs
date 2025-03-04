using FleetManager.BuildingBlocks.Domain.Results;

namespace FleetManager.Modules.Contractors.Domain
{
    internal class ContractorFactory
    {
        internal static Result<Contractor> Create(string name, Guid headquartersId)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Result<Contractor>.Failure(Errors.MissingContractorName());
            }

            if (headquartersId == Guid.Empty)
            {
                return Result<Contractor>.Failure(Errors.MissingHeadquarters());
            }

            return Result<Contractor>.Success(new Contractor(name, headquartersId));
        }
    }
}
