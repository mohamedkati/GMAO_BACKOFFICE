using GMAO.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.ValueObjects
{
    public class TenantSubscription : ValueObject
    {
        public SubscriptionPlan Plan { get; set; } = SubscriptionPlan.Free;
        public SubscriptionStatus Status { get; set; } = SubscriptionStatus.Inactive;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}
