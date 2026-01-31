using GMAO.Application.Helpers.Responses;
using MediatR;

namespace GMAO.Application.Features.sites.Commands.Occupants.CreateOccupant
{
    public class CreateOccupantCommandHandler : IRequestHandler<CreateOccupantCommand, ResponseResult<Guid>>
    {
        public async Task<ResponseResult<Guid>> Handle(CreateOccupantCommand request, CancellationToken cancellationToken)
        {
            // Implementation for creating an occupant goes here.
            // This is a placeholder implementation.
            Guid newOccupantId = Guid.NewGuid(); // Simulate creation and return new ID.
            return ResponseResult<Guid>.OkResult(newOccupantId);
        }
    }
}
