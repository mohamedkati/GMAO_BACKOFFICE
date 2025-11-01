using GMAO.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.Entities
{
    public class Tenant : BaseEntity<Guid>
    {
        public string Name { get; set; } = default!;
        public string Country { get; set; } = "Maroc";
        public string Currency { get; set; } = "MAD";
        public bool IsActive { get; set; } = true;

    }
}
