using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMAO.Domain.Common;

namespace GMAO.Domain.Entities.siteAggregate;

public class SiteKeeper : BaseAuditableEntity
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string CellPhone { get; set; }
    public string WorkingHours { get; set; }
    public string Location { get; set; }
    public string Notes { get; set; }
    public bool IsMainKeeper { get; set; }
    public virtual Site Site { get; set; }
    public Guid SiteId { get; set; }
}