using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Application.Helpers.Responses;
using GMAO.Domain.Entities;
using MediatR;

namespace GMAO.Application.Features.Customers.Commands.DeleteCustomerContact
{
    public class DeleteCustomerContactCommandHandler : IRequestHandler<DeleteCustomerContactCommand, ResponseResult<bool>>
    {
        private readonly IRepository<CustomerContact> _repository;

        public DeleteCustomerContactCommandHandler(IRepository<CustomerContact> repository)
        {
            this._repository = repository;
        }
        public async Task<ResponseResult<bool>> Handle(DeleteCustomerContactCommand request, CancellationToken cancellationToken)
        {
            var contact = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (contact == null)
            {
                return ResponseResult<bool>.FailResult("Le contact client n'existe pas.");
            }

            if (contact.CustomerId != request.CustomerId)
            {
                return ResponseResult<bool>.FailResult("Le contact client n'appartient pas à ce client.");
            }

            _repository.Remove(contact);
            await _repository.SaveChangesAsync(cancellationToken);

            return ResponseResult<bool>.OkResult(true, "Suppression du contact client réussie.");
        }
    }
}
