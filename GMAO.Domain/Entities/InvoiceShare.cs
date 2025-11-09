using GMAO.Domain.Common;

namespace GMAO.Domain.Entities
{
    public class InvoiceShare : BaseEntity<Guid>
    {
        public Guid InvoiceId { get; set; }
        public Invoice Invoice { get; set; } = null!;
        public Guid UnitId { get; set; }
        public Unit Unit { get; set; } = null!;
        public Guid? OccupantId { get; set; }
        public Occupant? Occupant { get; set; }
        public int Tantièmes { get; set; }
        public decimal SharePercent { get; set; }
        public decimal Amount { get; set; }
        public bool IsNotified { get; set; }
        public DateTime? NotifiedAt { get; set; }
    }
}