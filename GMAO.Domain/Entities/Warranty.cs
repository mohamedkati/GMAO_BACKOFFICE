using GMAO.Domain.Common;

namespace GMAO.Domain.Entities
{
    public class Warranty : BaseEntity<Guid>
    {
        public Guid AssetId { get; set; }
        public Asset Asset { get; set; } = null!;
        public WarrantyType Type { get; set; }
        public string ProviderName { get; set; } = string.Empty;
        public string? WarrantyNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive => DateTime.UtcNow >= StartDate && DateTime.UtcNow <= EndDate;
        public string? CoveredItems { get; set; }
        public string? Exclusions { get; set; }
        public string? ContactPhone { get; set; }
        public string? ContactEmail { get; set; }
        public int ClaimsCount { get; set; }
        public decimal ClaimedAmount { get; set; }
        public bool SendExpirationAlert { get; set; } = true;
        public int AlertDaysBefore { get; set; } = 60;
    }
}