using GMAO.Domain.Entities.siteAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Common.Interfaces.Repositories
{
    public interface IOccupantRepositoryAsync : IRepository<Occupant>
    {
        public Task<IReadOnlyList<Occupant>> GetOccupantsBySiteIdAsync(Guid siteId);
    }
}
