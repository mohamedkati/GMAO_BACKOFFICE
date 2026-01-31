using AutoMapper;
using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Application.Features.sites.DTOs;
using GMAO.Application.Helpers.Responses;
using MediatR;

namespace GMAO.Application.Features.sites.Queries.GetOccupantsBySiteId
{
    public class GetOccupantsBySiteIdQueryHandler : IRequestHandler<GetOccupantsBySiteIdQuery, ResponseResult<IReadOnlyList<OccupantUnitSite>>>
    {
        private readonly IOccupantRepositoryAsync _occupantRepository;
        private readonly IMapper _mapper;

        public GetOccupantsBySiteIdQueryHandler(IOccupantRepositoryAsync occupantRepository, IMapper mapper)
        {
            this._occupantRepository = occupantRepository;
            this._mapper = mapper;
        }

        public async Task<ResponseResult<IReadOnlyList<OccupantUnitSite>>> Handle(GetOccupantsBySiteIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _occupantRepository.GetOccupantsBySiteIdAsync(request.SiteId);
            var mappedOccupants = _mapper.Map<IReadOnlyList<OccupantUnitSite>>(result);
            return ResponseResult<IReadOnlyList<OccupantUnitSite>>.OkResult(mappedOccupants);
        }
    }
}
