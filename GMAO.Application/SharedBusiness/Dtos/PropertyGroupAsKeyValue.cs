using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.SharedBusiness.Dtos
{
    public class PropertyGroupAsKeyValue
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Reference { get; set; }
        public int Type { get; set; }
    }
}
