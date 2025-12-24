using GMAO.Domain.Common;
using GMAO.Domain.Entities.siteAggregate;

namespace GMAO.Domain.Entities
{
    public class ContactType : BaseEntity<Guid>
    {
        public string Name { get; set; } /// "Technique" | "Administratif" | "Urgence" | "Gardien"
        public int? Priority{ get; set; } // Pour ordre d'appel
        public ICollection<SiteContact> Contacts { get; set; } = new List<SiteContact>();
    }
}