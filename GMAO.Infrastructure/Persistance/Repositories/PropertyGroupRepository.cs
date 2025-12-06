using AutoMapper;
using AutoMapper.QueryableExtensions;
using Azure.Core;
using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Application.SharedBusiness.Dtos;
using GMAO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GMAO.Infrastructure.Persistance.Repositories
{
    public class PropertyGroupRepository : Repository<PropertyGroup>, IPropertyGroupRepository
    {
        public PropertyGroupRepository(AppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<Guid> CreateGroupContactAsync(PropertyGroupContact contact, CancellationToken cancellation = default!)
        {
            await _context.PropertyGroupsContacts.AddAsync(contact);
            await _context.SaveChangesAsync(cancellation);
            return contact.Id;
        }

        public async Task UpdateGroupContactAsync(PropertyGroupContact contact)
        {
            _context.PropertyGroupsContacts.Update(contact);
            await _context.SaveChangesAsync();
        }

        public async Task<PropertyGroupContact?> GetPropertyGroupContactByIdAsync(Guid id, CancellationToken cancellation = default!)
        {
            return await _context
                .PropertyGroupsContacts
                .FirstOrDefaultAsync(pgc => pgc.Id == id, cancellation);
        }

        public async Task<IReadOnlyList<TResult>> GetPropertyGroupContactsByIdAsync<TResult>(Guid id, CancellationToken cancellation = default!)
        {
            return await _context
                .PropertyGroupsContacts
                .Where(pgc => pgc.PropertyGroupId == id)
                .AsNoTracking()
                 .ProjectTo<TResult>(_mapper.ConfigurationProvider)
                 .ToListAsync(cancellation);
        }

        public async Task<(IReadOnlyList<TResult>, int)> GetPropertyGroupsAsync<TResult>(string? searchString, int? type, int? status, int? legalForm, CancellationToken cancellation = default!)
        {
            var query = _context.Set<PropertyGroup>().AsQueryable();
            if (type.HasValue)
                query = query.Where(x => (int)x.Type == type.Value);
            if (status.HasValue)
                query = query.Where(x => (int)x.Status == status.Value);
            if (legalForm.HasValue)
                query = query.Where(x => x.LegalForm != null && (int)x.LegalForm == legalForm.Value);

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                var searchLower = searchString.Trim().ToLower();
                query = query.Where(x =>
                    x.Reference.ToLower().Contains(searchLower) ||
                    x.Name.ToLower().Contains(searchLower) ||
                    (x.Description != null && x.Description.ToLower().Contains(searchLower)) ||
                    (x.LegalName != null && x.LegalName.ToLower().Contains(searchLower)) ||
                    (x.SIREN != null && x.SIREN.ToLower().Contains(searchLower)) ||
                    (x.CompanyRegistrationNumber != null && x.CompanyRegistrationNumber.ToLower().Contains(searchLower))
                );
            }
            var totalCount = await query.CountAsync(cancellation);
            return (
                await query
                .AsNoTracking()
                .ProjectTo<TResult>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellation),
                totalCount);
        }

        public async Task<bool> DeleteGroupContactAsync(Guid id, CancellationToken cancellationToken)
        {
            var groupContact = await _context.PropertyGroupsContacts.FirstOrDefaultAsync(pc => pc.Id == id);
            if (groupContact != null)
            {
                _context.PropertyGroupsContacts.Remove(groupContact);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            return false;
        }

        public async Task<(IReadOnlyList<TResult>, int)> GetPropertyGroupsAsync<TResult>(string? searchString, IEnumerable<int>? type, IEnumerable<int>? status, IEnumerable<int>? legalForm, CancellationToken cancellation = default)
        {
            var query = _context.Set<PropertyGroup>().AsQueryable();
            if (type?.Count() > 0)
                query = query.Where(x => type.Contains((int)x.Type));
            if (status?.Count() > 0)
                query = query.Where(x => status.Contains((int)x.Status));
            if (legalForm?.Count() > 0)
                query = query.Where(x => x.LegalForm != null && legalForm.Contains((int)x.LegalForm));

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                var searchLower = searchString.Trim().ToLower();
                query = query.Where(x =>
                    x.Reference.ToLower().Contains(searchLower) ||
                    x.Name.ToLower().Contains(searchLower) ||
                    (x.Description != null && x.Description.ToLower().Contains(searchLower)) ||
                    (x.LegalName != null && x.LegalName.ToLower().Contains(searchLower)) ||
                    (x.SIREN != null && x.SIREN.ToLower().Contains(searchLower)) ||
                    (x.CompanyRegistrationNumber != null && x.CompanyRegistrationNumber.ToLower().Contains(searchLower))
                );
            }
            var totalCount = await query.CountAsync(cancellation);
            return (
                await query
                .AsNoTracking()
                .ProjectTo<TResult>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellation),
                totalCount);
        }

        public async Task<IReadOnlyList<PropertyGroupAsKeyValue>> GetPropertyGroupsAsKeyValueAsync(string search, CancellationToken cancellation = default)
        {
            var query = _context.PropertyGroups.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
            {
                var searchLower = search.Trim().ToLower();
                query = query.Where(x =>
                    x.Reference.ToLower().Contains(searchLower) ||
                    x.Name.ToLower().Contains(searchLower)
                );
            }
            return await query
                .AsNoTracking()
                .ProjectTo<PropertyGroupAsKeyValue>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellation);
        }
    }
}
