using AutoMapper;
using GMAO.Application.Common.Exceptions;
using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Application.Features.Customers.DTOs;
using GMAO.Application.Helpers.Responses;
using GMAO.Domain.Entities;
using MediatR;

namespace GMAO.Application.Features.Customers.Queries.GetCustomerContactById
{
    public class GetCustomerContactByIdQueryHandler : IRequestHandler<GetCustomerContactByIdQuery, ResponseResult<CustomerContactDto>>
    {
        private readonly IRepository<CustomerContact> _repoCustomerContact;
        private readonly IMapper _mapper;

        public GetCustomerContactByIdQueryHandler(IRepository<CustomerContact> repoCustomerContact, IMapper mapper)
        {
            this._repoCustomerContact = repoCustomerContact;
            this._mapper = mapper;
        }
        public async Task<ResponseResult<CustomerContactDto>> Handle(GetCustomerContactByIdQuery request, CancellationToken cancellationToken)
        {
            var contact = await _repoCustomerContact.FirstOrDefaultAsync(x => x.Id == request.ContactId && x.CustomerId == request.CustomerId, cancellationToken);

            if (contact == null)
            {
                throw new NotFoundException("Contact", request.ContactId);
            }

            return ResponseResult<CustomerContactDto>.OkResult(_mapper.Map<CustomerContactDto>(contact));
        }
    }
}
