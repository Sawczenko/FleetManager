namespace FleetManager.Modules.Contractors.Domain;

internal class Contractor
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public Guid HeadquartersId { get; private set; }

    public Contractor(string name, Guid headquartersId)
    {
        Name = name;
        HeadquartersId = headquartersId;
    }
}