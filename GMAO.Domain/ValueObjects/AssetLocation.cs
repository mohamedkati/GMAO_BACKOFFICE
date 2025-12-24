using GMAO.Domain.Common;
using GMAO.Domain.Entities.siteAggregate;

namespace GMAO.Domain.ValueObjects
{
    public class AssetLocation : ValueObject
    {
        public Guid? PlanDocumentId { get; set; } // Lien vers plan
        public double? XPosition { get; set; } // Position X sur plan (%)
        public double? YPosition { get; set; } // Position Y sur plan (%)
        public string? LocationDescription { get; set; } // "Chaufferie, fond à gauche"
        public SiteDocument? PlanDocument { get; set; } 
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return PlanDocumentId ?? Guid.Empty;
            yield return XPosition ?? 0;
            yield return YPosition ?? 0;
        }
    }
}
