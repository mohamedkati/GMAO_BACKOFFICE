using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.Enums
{
    public enum SystemRole
    {
        SuperAdmin = 0,   // rôle global SaaS (non lié à un tenant)
        TenantAdmin = 1,  // admin d’une société (tenant)
        Manager = 2,
        Technician = 3,
        ClientUser = 4
    }
}
