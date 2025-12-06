using GMAO.Domain.Interfaces;

namespace GMAO.Application.Common.Interfaces.Infrastructure
{
    public interface IDomainEventDispatcher
    {
        Task DispatchDomainEventsAsync(IEnumerable<IDomainEvent> events, CancellationToken cancellationToken = default);
    }
}
