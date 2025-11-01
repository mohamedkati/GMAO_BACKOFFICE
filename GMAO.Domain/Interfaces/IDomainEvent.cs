using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.Interfaces
{
    public interface IDomainEvent
    {
        DateTime OccurredOn { get;  set; } 
        bool IsPublished { get; set; } 
    }
}
