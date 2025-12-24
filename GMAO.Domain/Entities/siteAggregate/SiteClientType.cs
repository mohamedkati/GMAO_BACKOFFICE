using GMAO.Domain.Common;

namespace GMAO.Domain.Entities.siteAggregate
{
    public class SiteClientType : BaseAuditableEntity
    {
        public string Code { get; set; }
        public  ICollection<Site> Sites { get; set; }
        public  SiteCategory SiteCategory { get; set; }
        public Guid SiteCategoryId { get; set; }
    }
}