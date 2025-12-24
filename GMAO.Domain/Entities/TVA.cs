using GMAO.Domain.Common;
using GMAO.Domain.Entities.siteAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.Entities
{
    public class TVA : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public float ValuRate { get; set; }

        public ICollection<Site> Sites { get; set; }
    }
}
