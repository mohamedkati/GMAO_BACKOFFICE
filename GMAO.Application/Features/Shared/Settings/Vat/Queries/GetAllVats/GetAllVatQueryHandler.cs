using AutoMapper;
using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Application.Features.sites.DTOs;
using GMAO.Application.Helpers.Responses;
using GMAO.Domain.Entities;
using MediatR;

namespace GMAO.Application.Features.Shared.Settings.Vat.Queries.GetAllVats
{
    public class GetAllVatQueryHandler : IRequestHandler<GetAllVatQuery, ResponseResult<IReadOnlyList<TVATypeDto>>>
    {
        private readonly IRepository<TVA> repository;
        private readonly IMapper mapper;

        public GetAllVatQueryHandler(IRepository<TVA> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<ResponseResult<IReadOnlyList<TVATypeDto>>> Handle(GetAllVatQuery request, CancellationToken cancellationToken)
        {
            if(request.Search != null)
            {
                var filteredVatTypes = await repository.FilterAsync(vt => vt.Code.Contains(request.Search, StringComparison.OrdinalIgnoreCase));
                var filteredVatTypesDto = mapper.Map<IReadOnlyList<TVATypeDto>>(filteredVatTypes);
                return ResponseResult<IReadOnlyList<TVATypeDto>>.OkResult(filteredVatTypesDto);
            }
            var vatTypesEntities = await repository.GetAllAsync(cancellationToken);
            return ResponseResult<IReadOnlyList<TVATypeDto>>.OkResult(mapper.Map<IReadOnlyList<TVATypeDto>>(vatTypesEntities));
        }
    }
}
