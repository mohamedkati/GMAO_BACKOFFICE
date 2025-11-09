using GMAO.Domain.Common;
using GMAO.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.Entities
{
    public class ServiceContract : BaseAuditableEntity
    {
        public string Reference { get; set; } = string.Empty;
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        public ContractType Type { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ContractStatus Status { get; set; }
        public bool AutoRenewal { get; set; }
        public PricingModel PricingModel { get; set; }
        public decimal? MonthlyAmount { get; set; }
        public decimal? AnnualAmount { get; set; }
        public ContractPricing? Pricing { get; set; }
        public ICollection<ContractConsumption> Consumptions { get; set; } = new List<ContractConsumption>();
    }
}
