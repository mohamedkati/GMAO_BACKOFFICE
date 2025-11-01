using GMAO.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.Entities
{
    public class ClientSite : BaseAuditableEntity
    {
        public Guid ClientId { get; set; }
        public Guid SiteId { get; set; }
        public Client Client { get; set; } = default!;
        public Site Site { get; set; } = default!;
    }
}
