using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.Enums
{
    public enum WorkOrderStatus
    {
        Requested = 0,
        Planned = 1,
        Assigned = 2,
        InProgress = 3,
        WaitingForParts = 4,
        WaitingForQuote = 5,
        Completed = 6,
        Closed = 7
    }

}
