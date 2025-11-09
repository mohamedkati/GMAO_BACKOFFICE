using GMAO.Domain.Common;

namespace GMAO.Domain.Entities
{
    public class ContractPricing : BaseAuditableEntity
    {
        public Guid ServiceContractId { get; set; }
        public ServiceContract ServiceContract { get; set; } = null!;
        public PricingModel Model { get; set; }
        public decimal? FixedMonthlyFee { get; set; }
        public int? IncludedHours { get; set; }
        public decimal? HourlyRateAfterIncluded { get; set; }
        public decimal? EmergencyCalloutFee { get; set; }
        public decimal? PartsMarkupPercent { get; set; }
    }
}