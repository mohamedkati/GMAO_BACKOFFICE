using AutoMapper;
using GMAO.Application.Common.Exceptions;
using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Application.Helpers.Responses;
using GMAO.Domain.Entities;
using MediatR;

namespace GMAO.Application.Features.Customers.Commands.UpdateCustomerContact
{
    public class UpdateCustomerContactCommandHandler : IRequestHandler<UpdateCustomerContactCommand, ResponseResult<bool>>
    {
        private readonly IRepository<CustomerContact> _repository;
        private readonly IMapper _mapper;

        public UpdateCustomerContactCommandHandler(IRepository<CustomerContact> repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public async Task<ResponseResult<bool>> Handle(UpdateCustomerContactCommand request, CancellationToken cancellationToken)
        {
            await ValidateCustomerContactFields(request, cancellationToken);

            var contact = await _repository.GetByIdAsync(request.Id, cancellationToken);
            var contactToUpdate = _mapper.Map(request, contact);
            _repository.Update(contactToUpdate!);
            await _repository.SaveChangesAsync(cancellationToken);

            return ResponseResult<bool>.OkResult(true, "Mise à jour du contact client réussie.");
        }

        private async Task ValidateCustomerContactFields(UpdateCustomerContactCommand request, CancellationToken cancellationToken)
        {
            var errors = new Dictionary<string, string[]>();
            var contact = await _repository.AnyAsync(c => c.Id == request.Id, cancellationToken);
            if (!contact)
            {
                errors.Add(nameof(UpdateCustomerContactCommand.Id), [$"Le contact client n'existe pas"]);
                throw new AppValidationException(errors);
            }

            var isEmailAlreadyTaken = await _repository.AnyAsync(c => c.Email == request.Email && c.Id != request.Id, cancellationToken);
            if (isEmailAlreadyTaken)
                errors.Add(nameof(UpdateCustomerContactCommand.Email), [$"L'email {request.Email} est déjà utilisé par un autre contact."]);

            var phoneIsAlreadyTaken = await _repository.AnyAsync(c => c.Phone == request.Phone && c.Id != request.Id && !string.IsNullOrEmpty(request.Phone), cancellationToken);
            if (phoneIsAlreadyTaken)
                errors.Add(nameof(UpdateCustomerContactCommand.Phone), [$"Le numéro de téléphone {request.Phone} est déjà utilisé par un autre contact."]);

            if (errors.Any())
                throw new AppValidationException(errors);
        }
    }
}
