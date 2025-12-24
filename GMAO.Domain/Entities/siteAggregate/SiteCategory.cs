using GMAO.Domain.Common;

namespace GMAO.Domain.Entities.siteAggregate
{
    public class SiteCategory : BaseAuditableEntity
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public  ICollection<SiteClientType> SiteClientTypes { get; set; }
    }
}