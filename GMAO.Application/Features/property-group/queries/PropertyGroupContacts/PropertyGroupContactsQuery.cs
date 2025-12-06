using GMAO.Application.Helpers.Responses;
using MediatR;

namespace GMAO.Application.Features.property_group.queries.PropertyGroupContacts
{
    public class PropertyGroupContactsQuery : IRequest<ResponseResult<IReadOnlyList<PropertyGroupContactsDto>>>
    {
        public Guid PropertyGroupId { get; set; }
    }
}
