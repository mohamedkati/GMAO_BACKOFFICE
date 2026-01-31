using AutoMapper;
using AutoMapper.QueryableExtensions;
using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Application.Features.Customers.Commands.CreateBudget;
using GMAO.Application.Features.Customers.DTOs;
using GMAO.Application.Features.Customers.Queries.GetCustomers;
using GMAO.Application.SharedBusiness.Dtos.customer;
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

        public async Task<IReadOnlyList<CustomerContactDto>> GetCustomerContactsAsync(Guid customerId, string? search)
        {
            var query = _context.CustomerContacts.AsQueryable();
            if (!string.IsNullOrEmpty(search))
                query = query.Where(c => c.FirstName.Contains(search) || c.LastName.Contains(search) || c.Email.Contains(search) || (c.Phone != null && c.Phone.Contains(search)));

            var contacts = await query
                .Where(c => c.CustomerId == customerId)
                .ProjectTo<CustomerContactDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return contacts;
        }

        public async Task<IReadOnlyList<CustomerForSelectControlDto>> GetCustomersForSelectControlAsync(string? search, int? pageSize, CancellationToken cancellationToken)
        {
            var query = _context.Customers.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                if (search.Contains(" "))
                {
                    var terms = search.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    foreach (var term in terms)
                    {
                        query = query.Where(c => c.CompanyName.Contains(term) || c.Reference.Contains(term) || c.InvoiceAddress.FirstAddressLine.Contains(term));
                    }
                }
                else
                    query = query.Where(c => c.CompanyName.Contains(search) || c.Reference.Contains(search));
            }

            if (pageSize.HasValue)
            {
                query = query.Take(pageSize.Value);
            }

            return await query.ProjectTo<CustomerForSelectControlDto>(_mapper.ConfigurationProvider)
                        .AsNoTracking()
                        .ToListAsync(cancellationToken);
        }

        public async Task<CustomerDetailedDto?> GetCustomerWithContactsAndBudgetsById(Guid id, CancellationToken cancellationToken = default)
        {
            var query = _context.Customers.Include(c => c.Contacts)
                                          .Include(c => c.MaintenanceBudgets)
                                          .Include(c => c.PaymentMethod)
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
               .Include(x => x.MaintenanceBudgets)
               .AsNoTracking()
               .AsQueryable();

            if (!string.IsNullOrEmpty(filter.Search))
                query = query.Where(c => c.CompanyName.Contains(filter.Search) || c.Reference.Contains(filter.Search));

            if (filter.PropertyGroupId.HasValue)
                query = query.Where(c => c.PropertyGroupId == filter.PropertyGroupId.Value);

            if (filter.CommercialId.HasValue)
                query = query.Where(c => c.CommercialId == filter.CommercialId.Value);

            if (filter.Types != null && filter.Types.Count > 0)
                query = query.Where(c => filter.Types.Contains(c.Type));

            if (!string.IsNullOrEmpty(filter.City))
                query = query.Where(c => c.InvoiceAddress.City.ToLower() == filter.City.ToLower());

            query = filter.SortBy?.ToLower() switch
            {
                "companyname" => filter.SortOrder == "desc"
                    ? query.OrderByDescending(c => c.CompanyName)
                    : query.OrderBy(c => c.CompanyName),
                "reference" => filter.SortOrder == "desc"
                    ? query.OrderByDescending(c => c.Reference)
                    : query.OrderBy(c => c.Reference),
                "type" => filter.SortOrder == "desc"
                    ? query.OrderByDescending(c => c.Type)
                    : query.OrderBy(c => c.Type),
                "city" => filter.SortOrder == "desc"
                    ? query.OrderByDescending(c => c.InvoiceAddress.City)
                    : query.OrderBy(c => c.InvoiceAddress.City),
                _ => query.OrderBy(c => c.CompanyName)
            };

            var count = await query.CountAsync();
            var customers = await query
                .Skip((filter.Page - 1) * filter.PageSize)
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
                    LastModified = c.LastModifiedAt,
                    CreatedAt = c.CreatedAt,
                    InvoiceCity = c.InvoiceAddress != null ? c.InvoiceAddress.City : string.Empty,
                    // Contact principal (SubQuery optimisée)
                    PrimaryContactEmail = c.Contacts.FirstOrDefault(ct => ct.IsPrimary) != null
                    ? c.Contacts.FirstOrDefault(ct => ct.IsPrimary).Email
                    : c.Contacts.FirstOrDefault() != null
                        ? c.Contacts.FirstOrDefault().Email
                        : null,
                    PrimaryContactPhone = c.Contacts.FirstOrDefault(ct => ct.IsPrimary) != null
                    ? c.Contacts.FirstOrDefault(ct => ct.IsPrimary).Phone
                    : c.Contacts.FirstOrDefault() != null
                        ? c.Contacts.FirstOrDefault().Phone
                        : null,
                    PrimaryContactName = c.Contacts.FirstOrDefault(ct => ct.IsPrimary) != null
                    ? c.Contacts.FirstOrDefault(ct => ct.IsPrimary).FirstName + " " + c.Contacts.FirstOrDefault(ct => ct.IsPrimary).LastName
                    : c.Contacts.FirstOrDefault() != null
                        ? c.Contacts.FirstOrDefault().FirstName + " " + c.Contacts.FirstOrDefault().LastName
                        : null,
                    // Budget total (Sum)
                    TotalBudget = c.MaintenanceBudgets.Sum(b => b.BudgetedAmount),
                })
                .ToListAsync();

            return (customers, count);
        }
    }
}
