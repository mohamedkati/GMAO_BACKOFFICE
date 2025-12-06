using GMAO.Application.Helpers.Responses;
using MediatR;

namespace GMAO.Application.Features.property_group.Commands.DeleteContactGroup
{
    public class DeleteContactGroupCommand : IRequest<ResponseResult<bool>>
    {
        public Guid Id { get; set; }
    }
}
