using GMAO.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.Entities
{
    public class AssetType : BaseEntity<Guid>
    {
        public string Name { get; set; } = default!;
        public string Manufacturer { get; set; } = default!;
        public string Model { get; set; } = default!;
    }
}
