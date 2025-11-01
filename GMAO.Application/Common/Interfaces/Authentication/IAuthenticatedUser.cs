using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Common.Interfaces.Authentication
{
    public interface IAuthenticatedUser
    {
        Guid UserId { get; }
        Guid TenantId { get; }

        bool IsAuthenticated();
        bool IsAccountConfirmed();
    }
}
