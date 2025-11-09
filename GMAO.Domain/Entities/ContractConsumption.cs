using GMAO.Domain.Common;

namespace GMAO.Domain.Entities
{
    public class ContractConsumption : BaseAuditableEntity
    {
        public Guid ServiceContractId { get; set; }
        public ServiceContract ServiceContract { get; set; } = null!;
        public int Month { get; set; }
        public int Year { get; set; }
        public int HoursConsumed { get; set; }
        public decimal AmountCharged { get; set; }
        public int WorkOrdersCount { get; set; }
    }
}