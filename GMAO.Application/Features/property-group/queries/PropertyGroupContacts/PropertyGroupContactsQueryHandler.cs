using AutoMapper;
using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Application.Helpers.Responses;
using MediatR;

namespace GMAO.Application.Features.property_group.queries.PropertyGroupContacts
{
    public class PropertyGroupContactsQueryHandler : IRequestHandler<PropertyGroupContactsQuery, ResponseResult<IReadOnlyList<PropertyGroupContactsDto>>>
    {
        private readonly IPropertyGroupRepository _groupRepository;
        private readonly IMapper _mapper;

        public PropertyGroupContactsQueryHandler(IPropertyGroupRepository groupRepository, IMapper mapper)
        {
            this._groupRepository = groupRepository;
            this._mapper = mapper;
        }
        public async Task<ResponseResult<IReadOnlyList<PropertyGroupContactsDto>>> Handle(PropertyGroupContactsQuery request, CancellationToken cancellationToken)
        {
            if (request.PropertyGroupId == Guid.Empty)
                return ResponseResult<IReadOnlyList<PropertyGroupContactsDto>>.FailResult("Invalid Property Group Id.");

            var contacts = await _groupRepository.GetPropertyGroupContactsByIdAsync<PropertyGroupContactsDto>(request.PropertyGroupId,cancellationToken);

            return ResponseResult<IReadOnlyList<PropertyGroupContactsDto>>.OkResult(contacts);
        }
    }
}
