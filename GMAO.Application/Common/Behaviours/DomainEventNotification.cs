using GMAO.Domain.Interfaces;
using MediatR;

namespace GMAO.Application.Common.Behaviours
{
    /// <summary>
    /// Generic Domain Event Notification for MediatR, wrapping a domain event.
    /// This like I link my domain events to MediatR notifications. like My domain event implements INotification, but in reality my domain events implement IDomainEvent.
    /// just to keep my domain layer clean from MediatR dependencies, I create this wrapper class to adapt my domain events to MediatR notifications.
    /// </summary>
    /// <typeparam name="TDomainEvent"></typeparam>
    public class DomainEventNotification<TDomainEvent> : INotification where TDomainEvent : IDomainEvent
    {
        public TDomainEvent DomainEvent { get; }
        public DomainEventNotification(TDomainEvent domainEvent)
        {
            DomainEvent = domainEvent;
        }
    }
}
