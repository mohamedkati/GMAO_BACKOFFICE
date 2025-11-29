using GMAO.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.ValueObjects
{
    public class TenantLimits : ValueObject
    {
        public int? Users { get; set; }
        public int? WorkOrders { get; set; }
        public int? Storage { get; set; } // en Go

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Users;
        }
    }
}
