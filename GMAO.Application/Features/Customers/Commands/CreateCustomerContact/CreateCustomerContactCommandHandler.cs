using AutoMapper;
using GMAO.Application.Common.Exceptions;
using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Application.Helpers.Responses;
using GMAO.Domain.Entities;
using MediatR;

namespace GMAO.Application.Features.Customers.Commands.CreateCustomerContact
{
    public class CreateCustomerContactCommandHandler : IRequestHandler<CreateCustomerContactCommand, ResponseResult<bool>>
    {
        private readonly IRepository<CustomerContact> _repoContact;
        private readonly IRepository<Customer> _repoCustomer;
        private readonly IMapper _mapper;

        public CreateCustomerContactCommandHandler(IRepository<CustomerContact> repoContact, IRepository<Customer> repoCustomer, IMapper mapper)
        {
            this._repoContact = repoContact;
            this._repoCustomer = repoCustomer;
            this._mapper = mapper;
        }
        public async Task<ResponseResult<bool>> Handle(CreateCustomerContactCommand request, CancellationToken cancellationToken)
        {
            await ValidateCustomerContactFields(request, cancellationToken);

            var contact = _mapper.Map<CustomerContact>(request);
            await _repoContact.AddAsync(contact, cancellationToken);
            await _repoContact.SaveChangesAsync(cancellationToken);

            return ResponseResult<bool>.OkResult(true, "Contact client créé avec succès.");
        }

        private async Task ValidateCustomerContactFields(CreateCustomerContactCommand request, CancellationToken cancellationToken)
        {
            var errors = new Dictionary<string, string[]>();
            var customerExist = await _repoCustomer.AnyAsync(c => c.Id == request.CustomerId, cancellationToken);
            if (!customerExist)
                errors.Add(nameof(CreateCustomerContactCommand.CustomerId), [$"Le client n'existe pas"]);

            var isEmailAlreadyTaken = await _repoContact.AnyAsync(c => c.Email == request.Email, cancellationToken);
            if (isEmailAlreadyTaken)
                errors.Add(nameof(CreateCustomerContactCommand.Email), [$"L'email {request.Email} est déjà utilisé par un autre contact."]);

            var phoneIsAlreadyTaken = await _repoContact.AnyAsync(c => c.Phone == request.Phone && !string.IsNullOrEmpty(request.Phone), cancellationToken);
            if (phoneIsAlreadyTaken)
                errors.Add(nameof(CreateCustomerContactCommand.Phone), [$"Le numéro de téléphone {request.Phone} est déjà utilisé par un autre contact."]);

            if (errors.Any())
                throw new AppValidationException(errors);
        }
    }
}
