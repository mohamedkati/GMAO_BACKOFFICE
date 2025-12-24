using GMAO.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.ValueObjects
{
    public class SiteAccess : ValueObject
    {
        public string? AccessCodes { get; set; }
        public string? KeyInstructions { get; set; }
        public bool? RequiresBadge { get; set; } = false; // "Badge visiteur à l'accueil"
        public string? WorkingHours { get; set; }
        public string? AccessRestrictions { get; set; }
        public string? SafetyRequirements { get; set; }
        public string? ParkingInfo { get; set; }
        public string? GeneralInstructions { get; set; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return AccessCodes ?? string.Empty;
            yield return KeyInstructions ?? string.Empty;
            yield return WorkingHours ?? string.Empty;
        }
    }
}
