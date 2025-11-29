using AutoMapper;
using AutoMapper.QueryableExtensions;
using GMAO.Application.Common.Interfaces;
using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Application.Helpers.Responses;
using GMAO.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GMAO.Application.Features.property_group.queries.ListAllPropertyGroups
{
    public class ListAllPropertyGroupsQueryHandler : IRequestHandler<ListAllPropertyGroupsQuery, PagedResponse<ListPropertyGroupDto>>
    {
        private readonly IPropertyGroupRepository _groupRepository;
        private readonly IMapper mapper;

        public ListAllPropertyGroupsQueryHandler(IPropertyGroupRepository repository, IMapper mapper)
        {
            this.mapper = mapper;
            this._groupRepository = repository;
        }
        public async Task<PagedResponse<ListPropertyGroupDto>> Handle(ListAllPropertyGroupsQuery request, CancellationToken cancellationToken)
        {
            var (items, totalCount) = await _groupRepository.GetPropertyGroupsAsync<ListPropertyGroupDto>(request.Search, request.Type, request.Status, request.LegalForm,cancellationToken); 
            return PagedResponse<ListPropertyGroupDto>.Success(items, totalCount, request.PageNumber, request.PageSize);
        }

    }
}
