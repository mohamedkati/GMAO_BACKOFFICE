using GMAO.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.ValueObjects
{
    public class SLAPolicy : ValueObject
    {
        public int ResponseTimeMinutes { get; init; }
        public int ResolutionTimeHours { get; init; }

        public DateTime CalculateResponseDeadline(DateTime startTime)
        {
            return startTime.AddMinutes(ResponseTimeMinutes);
        }

        public DateTime CalculateResolutionDeadline(DateTime startTime)
        {
            return startTime.AddHours(ResolutionTimeHours);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ResponseTimeMinutes;
            yield return ResolutionTimeHours;
        }
    }
}
