using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Application.Helpers.Responses;
using MediatR;

namespace GMAO.Application.Features.property_group.Commands.DeleteContactGroup
{
    public class DeleteContactGroupCommandHandler : IRequestHandler<DeleteContactGroupCommand, ResponseResult<bool>>
    {
        private readonly IPropertyGroupRepository _repository;
        public DeleteContactGroupCommandHandler(IPropertyGroupRepository repository)
        {
            _repository = repository;
        }
        public async Task<ResponseResult<bool>> Handle(DeleteContactGroupCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.DeleteGroupContactAsync(request.Id, cancellationToken);
            if (result)
            {
                return ResponseResult<bool>.OkResult(true, "Contact group deleted successfully.");
            }
            else
            {
                return ResponseResult<bool>.FailResult("Failed to delete contact group.");
            }
        }
    }
}
