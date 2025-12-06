using AutoMapper;
using AutoMapper.QueryableExtensions;
using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Application.Features.Customers.Commands.CreateBudget;
using GMAO.Application.Features.Customers.DTOs;
using GMAO.Application.Features.Customers.Queries.GetCustomers;
using GMAO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Infrastructure.Persistance.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(AppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<bool> AddBudgetToCustomer(CreateBudgetCommand budgetCommand)
        {
            var budget = _mapper.Map<MaintenanceBudget>(budgetCommand);
            await _context.MaintenanceBudgets.AddAsync(budget);
            var result = await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IReadOnlyList<CustomerBudgetDto>> GetCustomerBudgetsAsync(Guid customerId)
        {
            var budgets = await _context.MaintenanceBudgets
                .Where(b => b.CustomerId == customerId)
                .ProjectTo<CustomerBudgetDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return budgets;
        }

        public async Task<IReadOnlyList<CustomerContactDto>> GetCustomerContactsAsync(Guid customerId)
        {
            var contacts = await _context.CustomerContacts
                .Where(c => c.CustomerId == customerId)
                .ProjectTo<CustomerContactDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return contacts;
        }

        public async Task<CustomerDetailedDto?> GetCustomerWithContactsAndBudgetsById(Guid id, CancellationToken cancellationToken = default)
        {
            var query = _context.Customers.Include(c => c.Contacts)
                                          .Include(c => c.MaintenanceBudgets)
                                          .Where(c => c.Id == id)
                                          .ProjectTo<CustomerDetailedDto>(_mapper.ConfigurationProvider)
                                          .AsNoTracking();
            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<(IReadOnlyList<CustomerDto>, int)> GetFilteredCustomersAsync(GetCustomersQuery filter)
        {
            var query = _context.Customers
               .Include(x => x.PropertyGroup)
               .Include(x => x.Sites)
               .Include(x => x.Contacts)
               .AsQueryable();

            if (!string.IsNullOrEmpty(filter.SearchString))
                query = query.Where(c => c.CompanyName.Contains(filter.SearchString) || c.Reference.Contains(filter.SearchString));

            if (filter.PropertyGroupId.HasValue)
                query = query.Where(c => c.PropertyGroupId == filter.PropertyGroupId.Value);

            if (filter.CommercialId.HasValue)
                query = query.Where(c => c.CommercialId == filter.CommercialId.Value);

            if (filter.Type.HasValue)
                query = query.Where(c => c.Type == filter.Type.Value);

            var count = await query.CountAsync();
            var customers = await query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(c => new CustomerDto()
                {
                    Id = c.Id,
                    Reference = c.Reference,
                    CompanyName = c.CompanyName,
                    Type = c.Type,
                    PropertyGroupName = c.PropertyGroup != null ? c.PropertyGroup.Name : string.Empty,
                    CommercialName = c.Commercial != null ? $"{c.Commercial.FirstName} {c.Commercial.LastName}" : string.Empty,
                    ContactsCount = c.Contacts.Count,
                    SitesCount = c.Sites.Count,
                    lastModified = c.LastModifiedAt
                })
                .ToListAsync();

            return (customers, count);
        }
    }
}
