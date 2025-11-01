using GMAO.Domain.Common;

namespace GMAO.Domain.Entities
{
    public class EventReason : BaseAuditableEntity
    {
        public string Label { get; set; }
        public virtual ICollection<Event> Events { get; set; }
    }
}