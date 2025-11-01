using GMAO.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Common.Interfaces.Infrastructure
{
    public interface IDomainEventDispatcher
    {
        Task DispatchDomainEventsAsync(IEnumerable<IDomainEvent> events, CancellationToken cancellationToken=default);
    }
}
