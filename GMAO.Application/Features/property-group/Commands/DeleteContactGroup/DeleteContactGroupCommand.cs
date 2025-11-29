using GMAO.Application.Helpers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.property_group.Commands.DeleteContactGroup
{
    public class DeleteContactGroupCommand : IRequest<ResponseResult<bool>>
    {
        public Guid Id { get; set; }
    }
}
