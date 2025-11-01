using GMAO.Domain.Common;
using GMAO.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.Events
{
    public class StaffCreatedEvent : DomainEvent
    {
        public Guid UserId { get; }
        public Guid TeneantId { get; }
        public string Password { get; }
        public Guid TenantId { get; }
        public string Email { get; }
        public StaffCreatedEvent(Guid userId,
                                Guid teneantId,
                                string password,
                                string email)
        {
            UserId = userId;
            TeneantId = teneantId;
            Password = password;
            Email = email;
        }
    }
}
