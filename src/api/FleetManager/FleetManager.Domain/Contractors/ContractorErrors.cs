using FleetManager.Domain.SeedWork;

namespace FleetManager.Domain.Contractors
{
    public static class ContractorErrors
    {
        public static Error MissingContractorName() => new Error("Contractor.MissingContractorName", "Contractor name is missing.");

        public static Error MissingHeadquarters() => new Error("Contractor.MissingHeadquarters", "Headquarters is missing.");
    }
}
