using GMAO.Application.Common.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Infrastructure.Infras
{
    public class DatetimeService : IDatetimeService
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
