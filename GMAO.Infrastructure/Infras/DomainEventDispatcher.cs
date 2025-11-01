using GMAO.Application.Common.Behaviours;
using GMAO.Application.Common.Interfaces.Infrastructure;
using GMAO.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Infrastructure.Infras
{
    /// <summary>
    /// /// Dispatches domain events to their respective handlers using MediatR.
    /// Why this  ? To keep the domain entities clean and free from dependencies on MediatR or any other infrastructure concerns.
    /// So what is the exact solution ? We create a separate service, the DomainEventDispatcher, that is responsible for dispatching domain events, and we created an Interface called IDomainService and all my event should implement it, after that i created a MediatR notification handler (DomainEventNotification in application layer) that listens for these notifications and handles them accordingly. this way we achieve a clean separation of concerns, and our domain entities remain focused on business logic without being coupled to infrastructure details. My DomainEvent is like Implement INotification but not in a direct way.
    /// The flow works like this: Domain Event (IDomainEvent) created, then passed to DomainEventDispatcher, which wraps it in a DomainEventNotification and publishes it via MediatR. Handlers for DomainEventNotification then process the event.
    /// why i need this dispatcher interface ? to be able to inject it in my DbContext and publish events after saving changes.
    /// </summary>
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IMediator _mediator;

        public DomainEventDispatcher(IMediator mediator)
        {
            this._mediator = mediator;
        }
        public async Task DispatchDomainEventsAsync(IEnumerable<IDomainEvent> events, CancellationToken cancellationToken = default)
        {
            foreach (var domainEvent in events)
            {
                var correctHandler = typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType());
                var @event = Activator.CreateInstance(correctHandler, domainEvent);
                if (@event is not null)
                    await _mediator.Publish(@event);
            }
        }
    }
}
