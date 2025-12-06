using AutoMapper;
using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Application.SharedBusiness.Dtos.Staff;
using GMAO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Infrastructure.Persistance.Repositories
{
    public class UserRepository : Repository<Staff>, IUserRepository
    {
        public UserRepository(AppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<IReadOnlyList<StaffAsKeyValueDto>> GetUsersAsKeyValue(string role, string? search, CancellationToken cancelToken = default!)
        {
            var query = _context.Staffs
                .AsQueryable()
                .Include(x => x.Tenants)
                .ThenInclude(x => x.Role)
                .AsQueryable();

            if (!string.IsNullOrEmpty(role))
            {
                query = query.Where(s => s.Tenants.Any(x => x.Role.Name.ToLower() == role.ToLower()));
            }

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(s => s.FirstName.Contains(search) || s.LastName.Contains(search) || s.Email.Contains(search));
            }
            return await query.Select(s => new StaffAsKeyValueDto
            {
                Id = s.Id,
                FullName = s.FirstName + " " + s.LastName,
                Email = s.Email,
                IsActive = true // TODO
            })
            .ToListAsync(cancelToken);
        }
    }
}
