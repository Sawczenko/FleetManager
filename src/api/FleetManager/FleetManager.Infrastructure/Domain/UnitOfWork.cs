using FleetManager.Domain.SeedWork.Entities;
using FleetManager.Infrastructure.Data;
using FleetManager.Domain.SeedWork;
using MediatR;

namespace FleetManager.Infrastructure.Domain
{
    internal sealed class UnitOfWork : IUnitOfWork
    {
        private readonly FleetManagerDbContext _fleetManagerDbContext;
        private readonly IPublisher _publisher;

        public UnitOfWork(FleetManagerDbContext fleetManagerDbContext, IPublisher publisher)
        {
            _fleetManagerDbContext = fleetManagerDbContext;
            _publisher = publisher;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            await PublishDomainEventsAsync(cancellationToken);
            return await _fleetManagerDbContext.SaveChangesAsync(cancellationToken);
        }

        private async Task PublishDomainEventsAsync(CancellationToken cancellationToken)
        {
            var domainEvents = _fleetManagerDbContext.ChangeTracker
                .Entries<Entity>()
                .Select(entry => entry.Entity)
                .SelectMany(entity =>
                {
                    var domainEvents = entity.GetDomainEvents();

                    entity.ClearDomainEvents();

                    return domainEvents;
                })
                .ToList();

            foreach (var domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent, cancellationToken);
            }
        }
    }
}
