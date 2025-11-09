using GMAO.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.ValueObjects
{
    public class AssetReliabilityMetrics : ValueObject
    {
        public int TotalFailures { get; init; }
        public int TotalMaintenanceHours { get; init; }
        public double MTBF { get; init; } // Mean Time Between Failures (hours)
        public double MTTR { get; init; } // Mean Time To Repair (hours)
        public double AvailabilityPercent { get; init; }
        public double FailuresPerYear { get; init; } = 0;
        public DateTime? LastFailureDate { get; init; }
        public DateTime? LastMaintenanceDate { get; init; }

        public bool IsHighRisk => FailuresPerYear > 5;
        public bool RecommendReplacement => FailuresPerYear > 5 && AvailabilityPercent < 85;

        public static AssetReliabilityMetrics Empty => new AssetReliabilityMetrics
        {
            TotalFailures = 0,
            TotalMaintenanceHours = 0,
            MTBF = 0,
            MTTR = 0,
            AvailabilityPercent = 100,
            FailuresPerYear = 0
        };

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return TotalFailures;
            yield return TotalMaintenanceHours;
            yield return MTBF;
            yield return MTTR;
            yield return AvailabilityPercent;
        }
    }

}
