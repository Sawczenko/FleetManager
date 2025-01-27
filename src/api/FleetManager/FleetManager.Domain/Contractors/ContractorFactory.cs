using FleetManager.Domain.SeedWork.Results;

namespace FleetManager.Domain.Contractors
{
    public class ContractorFactory
    {
        public static Result<Contractor> Create(string name, Guid headquartersId)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Result<Contractor>.Failure(ContractorErrors.MissingContractorName());
            }

            if (headquartersId == Guid.Empty)
            {
                return Result<Contractor>.Failure(ContractorErrors.MissingHeadquarters());
            }

            return Result<Contractor>.Success(new Contractor(name, headquartersId));
        }
    }
}
