using FleetManager.BuildingBlocks.Domain;

namespace FleetManager.Modules.Contractors.Domain
{
    internal static class Errors
    {
        internal static Error MissingContractorName() => new Error("Contractor.MissingContractorName", "Contractor name is missing.");

        internal static Error MissingHeadquarters() => new Error("Contractor.MissingHeadquarters", "Headquarters is missing.");
    }
}
