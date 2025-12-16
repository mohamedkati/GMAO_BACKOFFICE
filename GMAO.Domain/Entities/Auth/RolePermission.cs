using GMAO.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.Entities.Auth
{
    public class RolePermission : BaseEntity<Guid>
    {
        public Guid RoleId { get; set; }
        public Guid PermissionId { get; set; }
        // Relations
        public Role Role { get; set; }
        public Permission Permission { get; set; }
    }
}
