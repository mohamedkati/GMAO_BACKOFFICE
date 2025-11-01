using GMAO.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.Common
{
    public abstract class BaseAuditableEntity : BaseEntity<Guid>
    {
        public Guid TenantId { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid? LastModifiedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }
      
    }
}
