using AutoMapper;
using GMAO.Application.Common.Interfaces;
using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Application.Helpers.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.property_group.queries.DetailedPropertyGroup
{
    public class GetPropertyGroupByIdQuery : IRequest<ResponseResult<DetailedPropertyGroupDto>>
    {
        public Guid Id { get; set; }
    }

    public class GetPropertyGroupByIdQueryHandler : IRequestHandler<GetPropertyGroupByIdQuery, ResponseResult<DetailedPropertyGroupDto>>
    {
        private readonly IPropertyGroupRepository _gourpRepository;
        private readonly IMapper _mapper;

        public GetPropertyGroupByIdQueryHandler(IPropertyGroupRepository repository, IMapper mapper)
        {
            this._gourpRepository = repository;
            this._mapper = mapper;
        }
        public async Task<ResponseResult<DetailedPropertyGroupDto>> Handle(GetPropertyGroupByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
            {
                return ResponseResult<DetailedPropertyGroupDto>.FailResult("Invalid Property Group Id.");
            }

            var propertyGroup = await _gourpRepository.GetByIdAsync(request.Id, cancellationToken);

            if (propertyGroup == null)
            {
                return ResponseResult<DetailedPropertyGroupDto>.FailResult("Property Group not found.");
            }

            var dto = _mapper.Map<DetailedPropertyGroupDto>(propertyGroup);
            return ResponseResult<DetailedPropertyGroupDto>.OkResult(dto);
        }
    }
}
