using GMAO.Application.Helpers.Responses;
using MediatR;

namespace GMAO.Application.Features.sites.Commands.SiteContacts.CreateSiteContact
{
    public class CreateSiteContactCommandHandler : IRequestHandler<CreateSiteContactCommand, ResponseResult<Guid>>
    {
        public async Task<ResponseResult<Guid>> Handle(CreateSiteContactCommand request, CancellationToken cancellationToken)
        {
            // Implementation for creating a site contact goes here.
            // This is a placeholder implementation.
            Guid newContactId = Guid.NewGuid(); // Simulate creation and return new ID.
            return ResponseResult<Guid>.OkResult(newContactId);
        }
    }
}
