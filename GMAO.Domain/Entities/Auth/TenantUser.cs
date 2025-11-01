using GMAO.Domain.Common;
using GMAO.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.Entities.Auth
{
    /// <summary>
    /// Lien User↔Tenant avec le Role dans ce tenant (UserId pointe vers Identity.Users).
    /// </summary>
    public class TenantUser : BaseEntity<Guid>
    {
        public Guid TenantId { get; private set; }
        public Guid UserId { get; private set; }   // Identity: ApplicationUser.Id
        public Guid RoleId { get; private set; }
        public Guid StaffId { get; private set; } // schema : Staff.Id

        public Tenant Tenant { get; private set; } = default!;
        public Role Role { get; private set; } = default!;
        public Staff User { get; private set; }

        private TenantUser() { }
        public TenantUser(Guid tenantId, Guid userId, Role role)
        {
            TenantId = tenantId;
            UserId = userId;
            RoleId = role.Id;
            Id = Guid.NewGuid();
        }
        public TenantUser(Guid tenantId, Guid userId, Guid roleId,Guid staffId)
        {
            TenantId = tenantId;
            UserId = userId;
            StaffId = staffId;
            RoleId = roleId;
            Id = Guid.NewGuid();
        }
    }
}
