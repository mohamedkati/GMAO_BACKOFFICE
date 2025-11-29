using AutoMapper;
using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Application.Helpers.Responses;
using MediatR;

namespace GMAO.Application.Features.property_group.Commands.UpdateContactGroup
{
    public class UpdateContactGroupCommandHandler : IRequestHandler<UpdateContactGroupCommand, ResponseResult<bool>>
    {
        private readonly IPropertyGroupRepository repository;
        private readonly IMapper mapper;

        public UpdateContactGroupCommandHandler(IPropertyGroupRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<ResponseResult<bool>> Handle(UpdateContactGroupCommand request, CancellationToken cancellationToken)
        {
            var contact = await repository.GetPropertyGroupContactByIdAsync(request.Id, cancellationToken);
            if (contact == null)
            {
                return ResponseResult<bool>.FailResult("Contact group not found.");
            }

            var propGroupId = contact.PropertyGroupId;
            var contactUpdated = mapper.Map(request, contact);
            contactUpdated.PropertyGroupId = propGroupId; // Ensure PropertyGroupId remains unchanged
            await repository.UpdateGroupContactAsync(contactUpdated);
            return ResponseResult<bool>.OkResult(true, "Contact group updated successfully.");
        }
    }
}
