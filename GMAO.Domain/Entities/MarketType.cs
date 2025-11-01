using GMAO.Domain.Common;

namespace GMAO.Domain.Entities
{
    public class MarketType : BaseAuditableEntity
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public  ICollection<Site> Sites { get; set; }
    }
}