using GMAO.Application.Common.Behaviours;
using GMAO.Application.Common.Interfaces.Authentication;
using GMAO.Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.Staff.EventHandlers
{
    /// <summary>
    /// Handles the StaffCreatedEvent to send a confirmation email after staff registration.
    /// </summary>
    public class StaffCreatedEventHandler : INotificationHandler<DomainEventNotification<StaffCreatedEvent>>
    {
        private readonly IAuthenticatedUser _authenticatedUser;
        private readonly IAccountService _accountService;

        public StaffCreatedEventHandler(IAuthenticatedUser authenticatedUser, IAccountService accountService)
        {
            this._authenticatedUser = authenticatedUser;
            this._accountService = accountService;
        }
        public async Task Handle(DomainEventNotification<StaffCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var staffCreatedEvent = notification.DomainEvent;
            if (staffCreatedEvent is not null)
            {
                await _accountService.SendConfirmEmailAfterRegistrationAsync(staffCreatedEvent.Email, staffCreatedEvent.Password);
            }
        }
    }
}
