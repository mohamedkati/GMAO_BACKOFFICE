using GMAO.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.Entities
{
    public class Supplier : BaseAuditableEntity
    {
        public string Name { get; set; } = default!;
        public string Contact { get; set; } = default!;
        public string Email { get; set; } = default!;
        public int LeadTimeDays { get; set; }
    }
}
