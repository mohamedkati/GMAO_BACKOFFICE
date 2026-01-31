using AutoMapper;
using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Application.Features.sites.DTOs;
using GMAO.Application.Helpers.Responses;
using GMAO.Domain.Entities.siteAggregate;
using MediatR;

namespace GMAO.Application.Features.Shared.Settings.SectorTypes.Queries
{
    public class GetAllSectorTypesQueryHandler : IRequestHandler<GetAllSectorTypesQuery, ResponseResult<IReadOnlyList<SectorTypeDto>>>
    {
        private readonly IRepository<SectorType> _repository;
        private readonly IMapper mapper;

        public GetAllSectorTypesQueryHandler(IRepository<SectorType> repository, IMapper mapper)
        {
            this._repository = repository;
            this.mapper = mapper;
        }
        public async Task<ResponseResult<IReadOnlyList<SectorTypeDto>>> Handle(GetAllSectorTypesQuery request, CancellationToken cancellationToken)
        {
            if(request.Search != null)
            {
                var filteredSectorTypes = await _repository.FilterAsync(st => st.Code.Contains(request.Search, StringComparison.OrdinalIgnoreCase));
                return ResponseResult<IReadOnlyList<SectorTypeDto>>.OkResult(mapper.Map<IReadOnlyList<SectorTypeDto>>(filteredSectorTypes));
            }
            var sectorTypes = await _repository.GetAllAsync(cancellationToken);

            return ResponseResult<IReadOnlyList<SectorTypeDto>>.OkResult(mapper.Map<IReadOnlyList<SectorTypeDto>>(sectorTypes));
        }
    }
}
