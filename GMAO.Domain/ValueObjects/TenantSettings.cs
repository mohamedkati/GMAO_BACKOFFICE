using GMAO.Domain.Common;
using GMAO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.ValueObjects
{
    public class TenantSettings : ValueObject
    {
        public ThemeMode? Theme { get; set; } = ThemeMode.System;
        public string? Language { get; set; }
        public string? Timezone { get; set; }
        public string? DateFormat { get; set; }
        public string? TimeFormat { get; set; }
        public string? Currency { get; set; }

        /// <summary>
        /// Jour de début de semaine (0 = Dimanche, 1 = Lundi, etc.)
        /// </summary>
        public int? WeekStartsOn { get; set; } = 1;
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Theme;
        }
    }
}
