using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Application.Features.Customers.DTOs;
using GMAO.Application.Helpers.Responses;
using MediatR;

namespace GMAO.Application.Features.Customers.Queries.GetCustomerContacts;

public class GetCustomerContactsQueryHandler : IRequestHandler<GetCustomerContactsQuery, ResponseResult<IReadOnlyList<CustomerContactDto>>>
{
    private readonly ICustomerRepository _repository;

    public GetCustomerContactsQueryHandler(ICustomerRepository repository)
    {
        this._repository = repository;
    }
    public async Task<ResponseResult<IReadOnlyList<CustomerContactDto>>> Handle(GetCustomerContactsQuery request, CancellationToken cancellationToken)
    {
        var contacts = await _repository.GetCustomerContactsAsync(request.CustomerId);

        return ResponseResult<IReadOnlyList<CustomerContactDto>>.OkResult(contacts);
    }
}

