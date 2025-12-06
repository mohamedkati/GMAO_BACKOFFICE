using GMAO.Application.Helpers.Responses;
using GMAO.Application.SharedBusiness.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.property_group.queries.GetPropertyGroupsSelectAsKeyValue
{
    public class GetPropertyGroupsSelectAsKeyValueQuery : IRequest<ResponseResult<IReadOnlyList<PropertyGroupAsKeyValue>>>
    {
        public string Search { get; set; }
    }
}
