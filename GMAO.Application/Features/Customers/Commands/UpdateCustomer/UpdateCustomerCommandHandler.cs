using AutoMapper;
using GMAO.Application.Common.Exceptions;
using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Application.Features.Customers.Commands.CreateCustomer;
using GMAO.Application.Helpers.Responses;
using GMAO.Domain.Entities;
using MediatR;

namespace GMAO.Application.Features.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandHandler(IRepository<Customer> _repoCustomer, IRepository<Staff> _repoStaff, IRepository<PaymentMethod> _repoPaymentMethod, IRepository<PropertyGroup> _propertyGroupRepo, IMapper _mapper) : IRequestHandler<UpdateCustomerCommand, ResponseResult<bool>>
    {
        public async Task<ResponseResult<bool>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            await ValidateCustomerFields(request, cancellationToken);

            var customer = await _repoCustomer.GetByIdAsync(request.Id, cancellationToken);

            var customerToUpdate = _mapper.Map(request, customer);
            _repoCustomer.Update(customerToUpdate!);
            await _repoCustomer.SaveChangesAsync(cancellationToken);

            return ResponseResult<bool>.OkResult(true, "Mise à jour du client réussie.");
        }

        private async Task ValidateCustomerFields(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var errors = new Dictionary<string, string[]>();
            var customer = await _repoCustomer.AnyAsync(x => x.Id == request.Id, cancellationToken);
            if (!customer)
            {
                errors.Add(nameof(UpdateCustomerCommand.Id), [$"Le client n'existe pas"]);
                throw new AppValidationException(errors);
            }

            var existingCustomer = await _repoCustomer.AnyAsync(c => c.Reference == request.Reference && c.Id != request.Id, cancellationToken);
            if (existingCustomer)
                errors.Add(nameof(UpdateCustomerCommand.Reference), [$"La référence {request.Reference} exist déjà"]);

            var existingCustomerWithName = await _repoCustomer.AnyAsync(c => c.CompanyName == request.CompanyName && c.Id != request.Id, cancellationToken);
            if (existingCustomerWithName)
                errors.Add(nameof(CreateCustomerCommand.CompanyName), [$"Le nom du client {request.CompanyName} exist déjà"]);

            if (request.PaymentMethodId.HasValue)
            {
                var paymentMethodExist = await _repoPaymentMethod.AnyAsync(c => c.Id == request.PaymentMethodId, cancellationToken);
                if (!paymentMethodExist)
                    errors.Add(nameof(UpdateCustomerCommand.PaymentMethodId), [$"La méthode de paiement n'existe pas"]);
            }

            var commercialExist = await _repoStaff.AnyAsync(c => c.Id == request.CommercialId, cancellationToken);
            if (!commercialExist)
                errors.Add(nameof(UpdateCustomerCommand.CommercialId), [$"Le commercial n'existe pas"]);

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
