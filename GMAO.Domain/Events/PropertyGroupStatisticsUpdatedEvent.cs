using GMAO.Domain.Common;

namespace GMAO.Domain.Events
{
    public class PropertyGroupStatisticsUpdatedEvent : DomainEvent
    {
        public Guid PropertyGroupId { get; }
        public decimal TotalAnnualRevenue { get; }

        public PropertyGroupStatisticsUpdatedEvent(Guid propertyGroupId, decimal totalAnnualRevenue)
        {
            PropertyGroupId = propertyGroupId;
            TotalAnnualRevenue = totalAnnualRevenue;
        }
    }
}
