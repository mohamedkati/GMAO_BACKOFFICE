using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Application.Helpers.Responses;
using GMAO.Application.SharedBusiness.Dtos;
using MediatR;

namespace GMAO.Application.Features.property_group.queries.GetPropertyGroupsSelectAsKeyValue
{
    public class GetPropertyGroupsSelectAsKeyValueQueryHandler : IRequestHandler<GetPropertyGroupsSelectAsKeyValueQuery, ResponseResult<IReadOnlyList<PropertyGroupAsKeyValue>>>
    {
        private readonly IPropertyGroupRepository _groupRepository;

        public GetPropertyGroupsSelectAsKeyValueQueryHandler(IPropertyGroupRepository groupRepository)
        {
            this._groupRepository = groupRepository;
        }
        public async Task<ResponseResult<IReadOnlyList<PropertyGroupAsKeyValue>>> Handle(GetPropertyGroupsSelectAsKeyValueQuery request, CancellationToken cancellationToken)
        {
            return ResponseResult<IReadOnlyList<PropertyGroupAsKeyValue>>.OkResult(
                await _groupRepository.GetPropertyGroupsAsKeyValueAsync(request.Search, cancellationToken)
            );
        }
    }
}
