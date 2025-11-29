using AutoMapper;
using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Application.Helpers.Responses;
using GMAO.Domain.Entities;
using MediatR;

namespace GMAO.Application.Features.property_group.Commands.CreateContactGroup
{
    public class CreateContactGroupCommandHandler : IRequestHandler<CreateContactGroupCommand, ResponseResult<Guid>>
    {
        private readonly IPropertyGroupRepository repository;
        private readonly IMapper _mapper;

        public CreateContactGroupCommandHandler(IPropertyGroupRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this._mapper = mapper;
        }
        public async Task<ResponseResult<Guid>> Handle(CreateContactGroupCommand request, CancellationToken cancellationToken)
        {
            var contact = _mapper.Map<CreateContactGroupCommand, PropertyGroupContact>(request);
            await repository.CreateGroupContactAsync(contact, cancellationToken);
            return ResponseResult<Guid>.OkResult(contact.Id, "Contact group created successfully.");
        }
    }
}
