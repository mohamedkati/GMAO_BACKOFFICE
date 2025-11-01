using GMAO.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.Events
{
    public class TenantUserAddedEvent : DomainEvent
    {
        public Guid TenantId { get; }
        public Guid UserId { get; }
        public Guid RoleId { get; }
        public TenantUserAddedEvent(Guid tenantId,
            Guid userId,
            Guid roleId)
        {
            TenantId = tenantId; UserId = userId; RoleId = roleId;
        }
    }
}
