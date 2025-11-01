using GMAO.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.Common
{
    public abstract class DomainEvent : IDomainEvent
    {
        protected DomainEvent()
        {
        }

        public DateTime OccurredOn { get;  set; } = DateTime.UtcNow;
        public bool IsPublished { get; set; } = false;
    }
}
