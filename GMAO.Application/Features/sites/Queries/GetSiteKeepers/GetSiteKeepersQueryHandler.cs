using AutoMapper;
using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Application.Features.sites.DTOs;
using GMAO.Application.Helpers.Responses;
using GMAO.Domain.Entities.siteAggregate;
using MediatR;

namespace GMAO.Application.Features.sites.Queries.GetSiteKeepers
{
    public class GetSiteKeepersQueryHandler : IRequestHandler<GetSiteKeepersQuery, ResponseResult<IReadOnlyList<SiteKeeperDto>>>
    {
        private readonly IRepository<SiteKeeper> _sitekeeperRepo;
        private readonly IMapper _mapper;

        public GetSiteKeepersQueryHandler(IRepository<SiteKeeper> sitekeeperRepo, IMapper mapper)
        {
            _sitekeeperRepo = sitekeeperRepo;
            this._mapper = mapper;
        }
        public async Task<ResponseResult<IReadOnlyList<SiteKeeperDto>>> Handle(GetSiteKeepersQuery request, CancellationToken cancellationToken)
        {
            var siteKeepers = await _sitekeeperRepo.FilterAsync(s=> s.SiteId == request.SiteId);
            var mappedSiteKeepers = _mapper.Map<IReadOnlyList<SiteKeeperDto>>(siteKeepers);
            return ResponseResult<IReadOnlyList<SiteKeeperDto>>.OkResult(mappedSiteKeepers);
        }
    }
}
