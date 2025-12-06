using AutoMapper;
using GMAO.Application.Common.Exceptions;
using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Application.Helpers.Responses;
using GMAO.Domain.Entities;
using MediatR;

namespace GMAO.Application.Features.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, ResponseResult<bool>>
    {
        private readonly IRepository<Customer> _customerRepo;
        private readonly IRepository<PaymentMethod> _paymentMethodRepo;
        private readonly IRepository<Staff> _repositoryStaff;
        private readonly IRepository<PropertyGroup> _propertyGroupRepo;
        private readonly IMapper _mapper;

        public CreateCustomerCommandHandler(IRepository<Customer> customerRepo, IRepository<PaymentMethod> paymentMethodRepo, IRepository<Staff> repositoryStaff, IRepository<PropertyGroup> PropertyGroupRepo, IMapper mapper)
        {
            _customerRepo = customerRepo;
            _paymentMethodRepo = paymentMethodRepo;
            _repositoryStaff = repositoryStaff;
            _propertyGroupRepo = PropertyGroupRepo;
            _mapper = mapper;
        }
        public async Task<ResponseResult<bool>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            await ValidateCustomerFields(request, cancellationToken);

            var customer = _mapper.Map<Customer>(request);
            await _customerRepo.AddAsync(customer, cancellationToken);
            await _customerRepo.SaveChangesAsync(cancellationToken);

            return ResponseResult<bool>.OkResult(true, "Customer created successfully.");
        }

        private async Task ValidateCustomerFields(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var errors = new Dictionary<string, string[]>();
            var existingCustomer = await _customerRepo.AnyAsync(c => c.Reference == request.Reference, cancellationToken);
            if (existingCustomer)
                errors.Add(nameof(CreateCustomerCommand.Reference), [$"La référence {request.Reference} exist déjà"]);

            var existingCustomerWithName = await _customerRepo.AnyAsync(c => c.CompanyName == request.CompanyName, cancellationToken);
            if (existingCustomerWithName)
                errors.Add(nameof(CreateCustomerCommand.CompanyName), [$"Le nom du client {request.CompanyName} exist déjà"]);

            if (request.PaymentMethodId.HasValue)
            {
                var paymentMethodExist = await _paymentMethodRepo.AnyAsync(c => c.Id == request.PaymentMethodId, cancellationToken);
                if (!paymentMethodExist)
                    errors.Add(nameof(CreateCustomerCommand.PaymentMethodId), [$"La méthode de paiement n'existe pas"]);
            }

            var commercialExist = await _repositoryStaff.AnyAsync(c => c.Id == request.CommercialId, cancellationToken);
            if (!commercialExist)
                errors.Add(nameof(CreateCustomerCommand.CommercialId), [$"Le commercial n'existe pas"]);

            if (request.PropertyGroupId.HasValue)
            {
                var propertyGroupExist = await _propertyGroupRepo.AnyAsync(c => c.Id == request.PropertyGroupId, cancellationToken);
                if (!propertyGroupExist)
                    errors.Add(nameof(CreateCustomerCommand.PropertyGroupId), [$"Le groupe de propriété n'existe pas"]);
            }

            if (errors.Any())
                throw new AppValidationException(errors);
        }
    }
}
