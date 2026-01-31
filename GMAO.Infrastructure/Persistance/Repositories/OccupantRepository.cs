using AutoMapper;
using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Domain.Entities.siteAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Infrastructure.Persistance.Repositories
{
    public class OccupantRepository : Repository<Occupant>, IOccupantRepositoryAsync
    {
        public OccupantRepository(AppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<IReadOnlyList<Occupant>> GetOccupantsBySiteIdAsync(Guid siteId)
        {
            var occupants = await _context.Occupants
                .Include(Occupant => Occupant.Unit)
                .Where(o => o.Unit.SiteId == siteId)
                .ToListAsync();

            return occupants;
        }
    }
}
