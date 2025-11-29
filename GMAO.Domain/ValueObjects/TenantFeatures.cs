using GMAO.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.ValueObjects
{
    public class TenantFeatures : ValueObject
    {
        public bool? Notifications { get; set; }
        public bool? Analytics { get; set; }
        public bool? Realtime { get; set; }
        public bool? MobileApp { get; set; }
        public bool? ApiAccess { get; set; }
        public bool? CustomBranding { get; set; }
        public bool? AdvancedReports { get; set; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Notifications;
        }
    }
}
