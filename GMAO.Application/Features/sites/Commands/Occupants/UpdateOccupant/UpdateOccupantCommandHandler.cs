using GMAO.Application.Helpers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.sites.Commands.Occupants.UpdateOccupant
{
    public class UpdateOccupantCommandHandler : IRequestHandler<UpdateOccupantCommand, ResponseResult<bool>>
    {
        public Task<ResponseResult<bool>> Handle(UpdateOccupantCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
