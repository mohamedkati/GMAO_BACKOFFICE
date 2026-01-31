using GMAO.Application.Features.Customers.Commands.CreateBudget;
using GMAO.Application.Features.Customers.DTOs;
using GMAO.Application.Features.Customers.Queries.GetCustomers;
using GMAO.Application.SharedBusiness.Dtos.customer;
using GMAO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Common.Interfaces.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<(IReadOnlyList<CustomerDto>, int)> GetFilteredCustomersAsync(GetCustomersQuery filter);

        Task<IReadOnlyList<CustomerContactDto>> GetCustomerContactsAsync(Guid customerId, string? search);

        Task<bool> AddBudgetToCustomer(CreateBudgetCommand budgetCommand);

        Task<IReadOnlyList<CustomerBudgetDto>> GetCustomerBudgetsAsync(Guid customerId);

        Task<CustomerDetailedDto?> GetCustomerWithContactsAndBudgetsById(Guid id, CancellationToken cancellationToken = default!);
        Task<IReadOnlyList<CustomerForSelectControlDto>> GetCustomersForSelectControlAsync(string? search, int? pageSize, CancellationToken cancellationToken);
    }
}
