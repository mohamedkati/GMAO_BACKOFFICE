using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.me.DTOs
{
    public class MeQueryDto
    {
        public MyInfoDto User { get; set; }

        public TenantDto Tenant { get; set; }
    }
}
