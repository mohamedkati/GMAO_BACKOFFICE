using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMAO.Domain.Common;

namespace GMAO.Domain.Entities.siteAggregate;

public class SiteContact : BaseAuditableEntity
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string CellPhone { get; set; }
    public string Note { get; set; }
    public string Fax { get; set; }
    public Guid PersonTypeId { get; set; }
    public Guid SiteContactCategoryId { get; set; }
    public  ContactType SiteContactCategory { get; set; }
    public  Site Site { get; set; }
    public Guid SiteId { get; set; }
    public bool IsPrimary { get; set; } = false;
    public string? AvailabilityHours { get; set; }
    public PreferredContactMethod PreferredContactMethod {  get; set; } 

}