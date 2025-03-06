namespace FleetManager.BuildingBlocks.Domain.Entities;

public abstract class Entity
{
    private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();

    public IReadOnlyList<IDomainEvent> GetDomainEvents()
    {
        return _domainEvents.ToList();
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}