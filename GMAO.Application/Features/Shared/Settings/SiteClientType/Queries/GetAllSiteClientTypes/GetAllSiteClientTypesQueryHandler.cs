using AutoMapper;
using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Application.Features.sites.DTOs;
using GMAO.Application.Helpers.Responses;
using GMAO.Domain.Entities.siteAggregate;
using MediatR;

namespace GMAO.Application.Features.Shared.Settings.SiteClientType.Queries.GetAllSiteClientTypes
{
    public class GetAllSiteClientTypesQueryHandler : IRequestHandler<GetAllSiteClientTypesQuery, ResponseResult<IReadOnlyList<SiteClientTypeDto>>>
    {
        private readonly IRepository<Domain.Entities.siteAggregate.SiteClientType> _repository;
        private readonly IMapper mapper;

        public GetAllSiteClientTypesQueryHandler(IRepository<Domain.Entities.siteAggregate.SiteClientType> repository, IMapper mapper)
        {
            this._repository = repository;
            this.mapper = mapper;
        }
        public async Task<ResponseResult<IReadOnlyList<SiteClientTypeDto>>> Handle(GetAllSiteClientTypesQuery request, CancellationToken cancellationToken)
        {
            if (request.Search != null)
            {
                var filteredSectorTypes = await _repository.FilterAsync(st => st.Code.Contains(request.Search, StringComparison.OrdinalIgnoreCase));
                return ResponseResult<IReadOnlyList<SiteClientTypeDto>>.OkResult(mapper.Map<IReadOnlyList<SiteClientTypeDto>>(filteredSectorTypes));
            }
            var sectorTypes = await _repository.GetAllAsync(cancellationToken);

            return ResponseResult<IReadOnlyList<SiteClientTypeDto>>.OkResult(mapper.Map<IReadOnlyList<SiteClientTypeDto>>(sectorTypes));
        }
    }
}
